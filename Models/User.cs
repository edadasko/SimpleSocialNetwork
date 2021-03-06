﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using SimpleSocialNetwork.ViewModels;

namespace SimpleSocialNetwork.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDay { get; set; }
        public Gender Gender { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public string School { get; set; }
        public string University { get; set; }

        public string JobPlace { get; set; }
        public string JobPosition { get; set; }

        public bool IsBlocked { get; set; }

        public ICollection<Friendship> IncomingFrienshipRequests { get; set; }
        public ICollection<Friendship> OutgoingFrienshipRequests { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }

        public ICollection<Message> MessageFrom { get; set; }
        public ICollection<Message> MessageTo { get; set; }

        [NotMapped]
        public int? Years
        {
            get
            {
                if (BirthDay == null)
                    return null;
                var today = DateTime.Today;
                var age = today.Year - BirthDay.Value.Year;
                if (BirthDay.Value.Date > today.AddYears(-age))
                    age--;
                return age;
            }
        }
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
        public List<User> Followers
        {
            get
            {
                List<User> followers = new List<User>();

                foreach (var request in IncomingFrienshipRequests.Where
                                  (f => f.Status == FriendshipStatus.Rejected ||
                                  f.Status == FriendshipStatus.Waiting).ToList())
                    followers.Add(request.RequestFrom);
                return followers;
            }
        }

        [NotMapped]
        public List<User> Following
        {
            get
            {
                List<User> following = new List<User>();

                foreach (var request in OutgoingFrienshipRequests.Where
                                  (f => f.Status == FriendshipStatus.Rejected ||
                                  f.Status == FriendshipStatus.Waiting).ToList())
                    following.Add(request.RequestTo);
                return following;
            }
        }


        [NotMapped]
        public List<Post> Photos => Posts.Where(p => p.Type == PostType.PhotoOnly).ToList();

        [NotMapped]
        public Post MainPhoto => Posts.SingleOrDefault(p => p.Type == PostType.MainPhoto);

        [NotMapped]
        public List<Post> WallPosts => Posts.Where(p => p.Type == PostType.Normal).Reverse().ToList();

        [NotMapped]
        public string MainPhotoPath
        {
            get
            {
                try
                {
                    return Posts.SingleOrDefault(p => p.Type == PostType.MainPhoto).Photos.ToList()[0].Image;
                }
                catch(NullReferenceException)
                {
                    return "~/images/no_photo.png";
                }
            }
        }

        public void ChangeInformation(UserInfoViewModel info)
        {
            Name = info.Name;
            Surname = info.Surname;
            BirthDay = info.BirthDay;
            Email = info.Email;
            PhoneNumber = info.PhoneNumber;
            Country = info.Country;
            City = info.City;
            Address = info.Address;
            School = info.School;
            University = info.University;
            JobPlace = info.JobPlace;
            JobPosition = info.JobPosition;
            Gender = info.Gender;
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

    public enum Gender
    {
        [Display(Name = "Мужской")]
        Male,
        [Display(Name = "Женский")]
        Female
    }
}
