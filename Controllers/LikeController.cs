using System;
using System.Collections.Generic;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.Controllers
{
    public class LikeController
    {
        IUsersRepository _repository;
        User _user;

        public LikeController(IUsersRepository repository)
        {
            _repository = repository;
            _user = ((List<User>)_repository.Users)[0];
        }

        public string Create(Post post)
        {
            var like = new Like()
            {
                Owner = _user,
                Post = post
            };

            _repository.Create(like);
            _repository.Save();
            return "Created";
        }

        public string Remove(Like like)
        {
            _repository.Remove(like);
            _repository.Save();
            return "Removed";
        }
    }
}
