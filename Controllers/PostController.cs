using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.Controllers
{
    public class PostController : Controller
    {
        const int pageSize = 5;

        IUsersRepository _repository;
        User _user;

        public PostController(IUsersRepository repository,
                              IHttpContextAccessor httpContextAccessor,
                              UserManager<User> userManager)
        {
            _repository = repository;
            var id = userManager.GetUserId(httpContextAccessor.HttpContext.User);
            _user = _repository.GetUserById(id);
        }

        public ActionResult Index(int postId)
        {
            var post = _repository.GetPostById(postId);
            return PartialView(post);
        }

        [Route("createPost/{text}")]
        public ActionResult Create(string text)
        {
            var post = new Post()
            {
                Owner = _user,
                Date = DateTime.Now,
                Text = text,
                Type = PostType.Normal
            };

            _repository.Create(post);
            _repository.Save();
            return RedirectToAction("PostsList");
        }

        [Route("removePost/{postId}")]
        public ActionResult Remove(int postId)
        {
            var post = _repository.GetPostById(postId);
            _repository.Remove(post);
            _repository.Save();
            return RedirectToAction("PostsList");
        }

        public PartialViewResult PostsList(string userId = null)
        {
            User user = userId == null ? _user : _repository.GetUserById(userId);
            _repository.GetUsersPosts(user);
            return PartialView(user.WallPosts);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewBag.LoggedUser = _user;
        }
    }
}
