using System;
using System.Collections.Generic;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.Controllers
{
    public class CommentController
    {
        IUsersRepository _repository;
        User _user;

        public CommentController(IUsersRepository repository)
        {
            _repository = repository;
            _user = ((List<User>)_repository.Users)[0];
        }

        public string Create(Post post, string text)
        {
            var comment = new Comment()
            {
                Owner = _user,
                Post = post,
                Text = text,
                Date = DateTime.Now
            };

            _repository.Create(comment);
            _repository.Save();
            return "Created";
        }

        public string Remove(Comment comment)
        {
            _repository.Remove(comment);
            _repository.Save();
            return "Removed";
        }
    }
}
