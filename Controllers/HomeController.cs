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
        AppDbContext db;
        public HomeController(AppDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var users = db.Users.Include(u => u.OutgoingFrienshipRequests).
                                 Include(u => u.IncomingFrienshipRequests).
                                 ToList();

            foreach (var user in users)
            {
                var friends = user.Friends;
            }

            db.Entry(users[0])
              .Collection(c => c.Posts)
              .Load();

            var posts = users[0].Posts;

            var friendships = db.Friendships.ToList();

            return View();
        }
    }
}
