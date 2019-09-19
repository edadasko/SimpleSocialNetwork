﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SimpleSocialNetwork.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDay { get; set; }

        public string Email { get; set; }
        public string MobiePhone { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public string School { get; set; }
        public string University { get; set; }

        public string JobPlace { get; set; }
        public string JobPosition { get; set; }


        public ICollection<Friendship> IncomingFrienshipRequests { get; set; }
        public ICollection<Friendship> OutgoingFrienshipRequests { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }

        public ICollection<Message> MessageFrom { get; set; }
        public ICollection<Message> MessageTo { get; set; }

        [NotMapped]
        public List<User> Friends
        {
            get
            {
                List<User> friends = new List<User>();

                foreach (var request in IncomingFrienshipRequests.Where
                                  (f => f.Status == FriendshipStatus.Confirmed).ToList())
                    friends.Add(request.RequestFrom);

                foreach (var request in OutgoingFrienshipRequests.Where
                                  (f => f.Status == FriendshipStatus.Confirmed).ToList())
                    friends.Add(request.RequestTo);

                return friends;
            }
        }


        [NotMapped]
        public List<Post> Photos => Posts.Where(p => p.Type == PostType.PhotoOnly).ToList();

        [NotMapped]
        public Post MainPhoto => Posts.Single(p => p.Type == PostType.MainPhoto);

        [NotMapped]
        public Dictionary<User, List<Message>> Messages
        {
            get
            {
                var groupedOutgoingMessages = MessageFrom.GroupBy(m => m.UserTo);
                var groupedIncomingMessages = MessageTo.GroupBy(m => m.UserFrom);
                var groupedMessages = groupedOutgoingMessages.Concat(groupedIncomingMessages)
                                                             .GroupBy(m => m.Key).ToList();
                if (groupedMessages.Count == 0)
                    return new Dictionary<User, List<Message>>();

                var uniqueMessagesGroups = new Dictionary<User, List<Message>>();
                foreach (var group in groupedMessages)
                {
                    var user = group.Key;
                    var messagesWithCurrentUser = new List<Message>();
                    var listGroup = group.ToList();
                    messagesWithCurrentUser.AddRange(listGroup[0].ToList());
                    if (listGroup.Count > 1)
                        messagesWithCurrentUser.AddRange(listGroup[1].ToList());
                    messagesWithCurrentUser = messagesWithCurrentUser.OrderByDescending(m => m.Date).ToList();
                    uniqueMessagesGroups.Add(user, messagesWithCurrentUser);
                }
                return uniqueMessagesGroups;
            }
            
        }

        public User()
        {
            IncomingFrienshipRequests = new List<Friendship>();
            OutgoingFrienshipRequests = new List<Friendship>();
            Posts = new List<Post>();
            Comments = new List<Comment>();
            Likes = new HashSet<Like>();
            MessageFrom = new List<Message>();
            MessageTo = new List<Message>();
        }

        public override string ToString() => Name + " " + Surname;
    }
}