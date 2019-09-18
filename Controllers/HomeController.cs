using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        IUsersRepository _repository;
        public HomeController(IUsersRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            var users = _repository.Users.ToList();
            var messages = _repository.GetUsersMessages(users[0]);
            var news = _repository.GetUsersNews(users[1]);

            return View();
        }
    }
}
