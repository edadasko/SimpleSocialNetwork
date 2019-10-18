using System.Collections.Generic;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.ViewModels
{
    public class FriendsRequestsViewModel
    {
        public List<User> Friends { get; set; }
        public List<User> Followers { get; set; }
        public List<User> Following { get; set; }
        public List<User> Requests { get; set; }

    }
}
