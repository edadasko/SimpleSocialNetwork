﻿using System;
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

        public ViewResult MainPage(int userId)
        {
            User user = _repository.GetUserById(userId);
            _repository.GetUsersMainPageInfo(user);
            return View("Index", user);
        }

        public ViewResult Friends()
        {
            var friends =_repository.GetUsersFriends(_user);
            foreach(var friend in friends)
                _repository.GetUsersMainPhoto(friend);
            return View(friends);
        }

        public ViewResult News()
        {
            var news = _repository.GetUsersNews(_user);
            return View(news);
        }
    }
}