using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.Controllers
{
    public class NewsController : Controller
    {
        const int pageSize = 5;
        IUsersRepository _repository;
        User _user;

        public NewsController(IUsersRepository repository,
                              IHttpContextAccessor httpContextAccessor,
                              UserManager<User> userManager)
        {
            _repository = repository;
            var id = userManager.GetUserId(httpContextAccessor.HttpContext.User);
            _user = _repository.GetUserById(id);
        }

        public ActionResult Index(int page = 0)
        {
            if (_user.IsBlocked)
                return View("NoAccess", "Home");

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
