using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.Controllers
{
    public class NewsController : Controller
    {
        IUsersRepository _repository;
        const int pageSize = 5;
        User _user;

        public NewsController(IUsersRepository repository)
        {
            _repository = repository;
            _user = ((List<User>)_repository.Users)[0];
        }

        public ActionResult Index(int page = 0)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("~/Views/Post/PostsList.cshtml", GetItemsPage(page));
            }
            return View(GetItemsPage(page));
        }

        private List<Post> GetItemsPage(int page)
        {
            var itemsToSkip = page * pageSize;
            var news = _repository.GetUsersNews(_user).Skip(itemsToSkip).Take(pageSize).ToList();
            return news;
        }

    }
}
