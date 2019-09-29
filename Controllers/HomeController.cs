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
        public RedirectToActionResult Index()
        {
            return RedirectToAction("Index", "User");
        }
    }
}
