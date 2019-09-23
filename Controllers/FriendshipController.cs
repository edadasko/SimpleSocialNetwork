using System.Collections.Generic;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.Controllers
{
    public class FriendshipController
    {
        IUsersRepository _repository;
        User _user;

        public FriendshipController(IUsersRepository repository)
        {
            _repository = repository;
            _user = ((List<User>)_repository.Users)[0];
        }

        public string AddToFriends (User user)
        {
            var request = new Friendship()
            {
                RequestFrom = _user,
                RequestTo = user,
                Status = FriendshipStatus.Waiting
            };

            _repository.Create(request);
            _repository.Save();
            return "Request has been sent to " + user;
        }

        public string ConfirmRequest(Friendship request)
        {
            request.Status = FriendshipStatus.Confirmed;
            _repository.Update(request);
            return request.RequestFrom + " added to friends";
        }

        public string RemoveFromFriends(Friendship request)
        {
            request.Status = FriendshipStatus.Rejected;
            _repository.Update(request);
            return request.RequestFrom + " removed from friends";
        }
    }
}
