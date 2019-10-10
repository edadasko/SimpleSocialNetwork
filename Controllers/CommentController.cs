using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.Controllers
{
    public class CommentController: Controller
    {
        IUsersRepository _repository;
        User _user;

        public CommentController(IUsersRepository repository)
        {
            _repository = repository;
            _user = ((List<User>)_repository.Users)[0];
        }

        [Route("addComment/{text}/{postId}")]
        public RedirectToActionResult Create(string text, int postId)
        {
            var comment = new Comment()
            {
                Owner = _user,
                Post = _repository.GetPostById(postId),
                Text = text,
                Date = DateTime.Now
            };

            _repository.Create(comment);
            _repository.Save();

            return RedirectToAction("CommentsList", new { postId });
        }


        [Route("removeComment/{commentId}")]
        public RedirectToActionResult Remove(int commentId)
        {
            var comment = _repository.GetCommentById(commentId);
            var postId = comment.Post.Id;
            _repository.Remove(comment);
            _repository.Save();
            return RedirectToAction("CommentsList", new { postId });
        }


        public PartialViewResult CommentsList(int postId)
        {
            Post post = _repository.GetPostById(postId);
            return PartialView(post.Comments);
        }
    }
}
