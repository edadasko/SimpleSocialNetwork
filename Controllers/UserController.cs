using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.Controllers
{
    public class UserController : Controller
    {
        IUsersRepository _repository;
        User _user;
        public UserController(IUsersRepository repository)
        {
            _repository = repository;
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

        public ActionResult Posts()
        {
            _repository.GetUsersPosts(_user);
            return PartialView(_user.WallPosts);
        }

        public ViewResult Friends(int userId)
        {
            User user = userId == 0 ? _user:_repository.GetUserById(userId);
            var friends =_repository.GetUsersFriends(user);
            foreach(var friend in friends)
                _repository.GetUsersMainPhoto(friend);
            return View(friends);
        }

        public ViewResult News()
        {
            var news = _repository.GetUsersNews(_user);
            return View(news.Where(p => p.Type == PostType.Normal).ToList());
        }
    }
}
