using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleSocialNetwork.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using SimpleSocialNetwork.ViewModels;

namespace SimpleSocialNetwork.Controllers
{
    public class UserController : Controller
    {
        IUsersRepository _repository;
        IHostingEnvironment _environment;
        User _user;

        public UserController(IUsersRepository repository, IHostingEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
            _user = ((List<User>)_repository.Users)[0];
        }

        public ViewResult Index()
        {
            _repository.GetUsersMainPageInfo(_user);
            return View(_user);
        }

        public ActionResult MainPage(int userId)
        {
            if(userId == _user.UserId)
                return Redirect("~/User");
            User user = _repository.GetUserById(userId);
            _repository.GetUsersMainPageInfo(user);
            return View("Index", user);
        }


        public ViewResult Friends(int userId)
        {
            User user = userId == 0 ? _user:_repository.GetUserById(userId);

            var friends =_repository.GetUsersFriends(user);
            foreach(var friend in friends)
                _repository.GetUsersMainPhoto(friend);
            FriendsRequestsViewModel friendsVM = new FriendsRequestsViewModel
            {
                Friends = user.Friends
            };

            if (user == _user)
                friendsVM.Requests = user.IncomingFrienshipRequests.Where(r => r.Status == FriendshipStatus.Waiting)
                                                     .Select(r => r.RequestFrom).ToList();
            foreach (var r in friendsVM.Requests)
                _repository.GetUsersMainPhoto(r);
            return View(friendsVM);
        }

        public ViewResult News()
        {
            var likedPostsId = _repository.GetUsersLikes(_user).Select(l => l.PostId).ToHashSet();
            ViewBag.Likes = likedPostsId;
            var news = _repository.GetUsersNews(_user);
            return View(news.Where(p => p.Type == PostType.Normal).ToList());
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
            string path = "/usersPhotos/avatar" + _user.UserId + ".jpg";
            using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
            {
                await avatar.CopyToAsync(fileStream);
            }

            var prevPhoto = _repository.GetUsersMainPhoto(_user);
            _repository.Remove(prevPhoto);

            Post mainPhoto = new Post { Owner = _user, Date = DateTime.Now, Type = PostType.MainPhoto };
            Photo photo = new Photo { Image = path, Post = mainPhoto };
            _repository.Create(mainPhoto);
            _repository.Create(photo);

            _repository.Update(_user);
            _repository.Save();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewBag.User = _user;
        }


    }
}
