using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SimpleSocialNetwork.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
using SimpleSocialNetwork.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace SimpleSocialNetwork.Controllers
{
    public class UserController : Controller
    {
        IUsersRepository _repository;
        IHostingEnvironment _environment;
        User _user;
        UserManager<User> _userManager;

        public UserController(IUsersRepository repository,
                              IHostingEnvironment environment,
                              IHttpContextAccessor httpContextAccessor,
                              UserManager<User> userManager)
        {
            _repository = repository;
            _environment = environment;
            var id = userManager.GetUserId(httpContextAccessor.HttpContext.User);
            _user = _repository.GetUserById(id);
            _userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            _repository.GetUsersMainPageInfo(_user);
            var loggedRoles = (await _userManager.GetRolesAsync(_user)).ToList();
            ViewBag.LoggedUserRoles = loggedRoles;
            ViewBag.UserRoles = loggedRoles;
            if (_user.IsBlocked)
                return RedirectToAction("NoAccess", "Home");
            return View(_user);
        }

        public async Task<ActionResult> MainPage(string userId)
        {
            if(userId == _user.Id)
                return Redirect("~/User");
            User user = _repository.GetUserById(userId);
            _repository.GetUsersMainPageInfo(user);
            var roles = (await _userManager.GetRolesAsync(user)).ToList();
            var loggedRoles = (await _userManager.GetRolesAsync(_user)).ToList();
            ViewBag.LoggedUserRoles = loggedRoles;
            ViewBag.UserRoles = roles;
            if (_user.IsBlocked)
                return RedirectToAction("NoAccess", "Home");
            if (user.IsBlocked)
                return View("BlockedPage", user);
            return View("Index", user);
        }


        public ViewResult Friends(string userId = null)
        {
            if (_user.IsBlocked)
                return View("NoAccess", "Home");
            User user = userId == null ? _user:_repository.GetUserById(userId);

            var friends =_repository.GetUsersFriends(user);
            foreach(var friend in friends)
                _repository.GetUsersMainPhoto(friend);
            FriendsRequestsViewModel friendsVM = new FriendsRequestsViewModel
            {
                Friends = user.Friends
            };

            if (user == _user)
            {
                friendsVM.Requests = user.IncomingFrienshipRequests.Where(r => r.Status == FriendshipStatus.Waiting)
                                                     .Select(r => r.RequestFrom).ToList();
                foreach (var r in friendsVM.Requests)
                    _repository.GetUsersMainPhoto(r);
            }
            return View(friendsVM);
        }

        [HttpGet]
        public ViewResult Update()
        {
            return View(new UserInfoViewModel(_user));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> Update(UserInfoViewModel info)
        {
            if (info.Avatar != null)
            {
                await UpdateAvatar(info.Avatar);
            }

            _user.ChangeInformation(info);
            _repository.Update(_user);
            _repository.Save();
            return RedirectToAction("Index");
        }

        [NonAction]
        public async Task UpdateAvatar(IFormFile avatar)
        {
            string path = "/usersPhotos/avatar" + _user.Id + ".jpg";
            using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
            {
                await avatar.CopyToAsync(fileStream);
            }

            var prevPhoto = _repository.GetUsersMainPhoto(_user);
            if (prevPhoto != null)
                _repository.Remove(prevPhoto);

            Post mainPhoto = new Post { Owner = _user, Date = DateTime.Now, Type = PostType.MainPhoto };
            Photo photo = new Photo { Image = path, Post = mainPhoto };
            _repository.Create(mainPhoto);
            _repository.Create(photo);

            _repository.Update(_user);
            _repository.Save();
        }

        [Authorize(Roles = "moderator")]
        public Task<ActionResult> Block(string userId)
        {
            User user = _repository.GetUserById(userId);
            user.IsBlocked = true;
            _repository.Update(user);
            _repository.Save();
            return MainPage(userId);
        }

        [Authorize(Roles = "moderator")]
        public Task<ActionResult> Unblock(string userId)
        {
            User user = _repository.GetUserById(userId);
            user.IsBlocked = false;
            _repository.Update(user);
            _repository.Save();
            return MainPage(userId);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewBag.LoggedUser = _user;
        }
    }
}