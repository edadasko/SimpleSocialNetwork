using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.Controllers
{
    public class PostController : Controller
    {
        IUsersRepository _repository;
        User _user;

        const int pageSize = 5;

        public PostController(IUsersRepository repository)
        {
            _repository = repository;
            _user = ((List<User>)_repository.Users)[0];
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

        public PartialViewResult PostsList(int? userId = null)
        {
            User user = userId == null ? _user : _repository.GetUserById(userId.Value);
            _repository.GetUsersPosts(user);
            return PartialView(user.WallPosts);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewBag.User = _user;
        }
    }
}
