using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.Controllers
{
    public class LikeController: Controller
    {
        IUsersRepository _repository;
        User _user;

        public LikeController(IUsersRepository repository,
                              IHttpContextAccessor httpContextAccessor,
                              UserManager<User> userManager)
        {
            _repository = repository;
            var id = userManager.GetUserId(httpContextAccessor.HttpContext.User);
            _user = _repository.GetUserById(id);
        }

        [Route("addLike/{postId}")]
        public ActionResult Create(int postId)
        {
            var post = _repository.GetPostById(postId);
            if (!_user.Likes.Select(l => l.PostId).Contains(postId))
            {
                var like = new Like()
                {
                    Owner = _user,
                    Post = post
                };

                _repository.Create(like);
                _repository.Save();
            }
            return RedirectToAction("Index", "Post", new { postId });
        }

        [Route("removeLike/{postId}")]
        public ActionResult Remove(int postId)
        {
            var post = _repository.GetPostById(postId);
            var like = post.Likes.Single(l => l.Owner == _user);
            _repository.Remove(like);
            _repository.Save();
            return RedirectToAction("Index", "Post", new { postId });
        }
    }
}
