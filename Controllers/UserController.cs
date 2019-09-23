using System;
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

        public string Index(int id)
        {
            var user = id == 0 ? _user: _repository.GetUserById(id);
            if (user == null)
                return "";
            string info = "";

            info += "Дата рождения: " + user.BirthDay?.ToString("dd.MM.yyyy") + "\n";
            info += "Страна: " + user.Country + "\n";
            info += "Город: " + user.City + "\n";

            var posts = _repository.GetUsersPosts(user);
            return user + "\n\n" + info + "\n\n" + string.Join("\n", posts); 
        }

        public string Friends(int id)
        {
            var user = id == 0 ? _user : _repository.GetUserById(id);
            var friends =_repository.GetUsersFriends(user);
            return string.Join("\n", friends);
        }

        public string News()
        {
            var news = new List<Post>();
            var friends = _repository.GetUsersFriends(_user);
            foreach (var friend in friends)
            {
                news.AddRange(_repository.GetUsersPosts(friend));
            }
            return string.Join("\n", news);
        }
    }
}