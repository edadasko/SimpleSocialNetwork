using System.Collections.Generic;
using SimpleSocialNetwork.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SimpleSocialNetwork.Controllers
{
    public class FriendshipController : Controller
    {
        IUsersRepository _repository;
        User _user;

        public FriendshipController(IUsersRepository repository)
        {
            _repository = repository;
            _user = ((List<User>)_repository.Users)[0];
        }

        public RedirectToActionResult AddToFriends(int userId)
        {
            var user = _repository.GetUserById(userId); 
            var request = new Friendship()
            {
                RequestFrom = _user,
                RequestTo = user,
                Status = FriendshipStatus.Waiting
            };

            _repository.Create(request);
            _repository.Save();
            return RedirectToAction("MainPage", "User", new { userId });
        }


        public RedirectToActionResult ConfirmRequest(int userId)
        {
            var request = GetRequest(userId);
            request.Status = FriendshipStatus.Confirmed;
            _repository.Update(request);
            _repository.Save();
            return RedirectToAction("MainPage", "User", new { userId });
        }

        public RedirectToActionResult CancelRequest(int userId)
        {
            var request = GetRequest(userId);
            _repository.Remove(request);
            _repository.Save();
            return RedirectToAction("MainPage", "User", new { userId });
        }

        public RedirectToActionResult Remove(int userId)
        {
            var request = GetRequest(userId);
            request.Status = FriendshipStatus.Rejected;
            _repository.Update(request);
            _repository.Save();
            return RedirectToAction("MainPage", "User", new { userId });
        }

        [NonAction]
        private Friendship GetRequest(int userId)
        {
            var user = _repository.GetUserById(userId);
            return _repository.Friendships.Single(f =>
                    (f.RequestFrom == _user && f.RequestTo == user) ||
                    (f.RequestFrom == user && f.RequestTo == _user));
        }

        public RedirectToActionResult ConfirmRequestFromFriendsView(int userId)
        {
            var request = GetRequest(userId);
            request.Status = FriendshipStatus.Confirmed;
            _repository.Update(request);
            _repository.Save();
            return RedirectToAction("Friends", "User");
        }

        public RedirectToActionResult CancelRequestFromFriendsView(int userId)
        {
            var request = GetRequest(userId);
            request.Status = FriendshipStatus.Rejected;
            _repository.Update(request);
            _repository.Save();
            return RedirectToAction("Friends", "User");
        }
    }
}
