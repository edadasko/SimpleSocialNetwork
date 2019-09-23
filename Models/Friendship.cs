using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleSocialNetwork.Models
{
    public enum FriendshipStatus
    {
        Confirmed,
        Waiting,
        Rejected
    }

    public class Friendship
    {
        public int Id { get; set; }

        [InverseProperty("OutgoingFrienshipRequests")]
        public User RequestFrom { get; set; }

        [InverseProperty("IncomingFrienshipRequests")]
        public User RequestTo { get; set; }

        public FriendshipStatus Status { get; set; }
    }
}
