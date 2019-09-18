using System;
using System.Collections.Generic;
using System.Linq;
using SimpleSocialNetwork.Models;
namespace SimpleSocialNetwork
{
    public class SeedData
    {
        public static void Initialize(UsersContext context)
        {
            FakeUsersRepository rep = new FakeUsersRepository();

            if (!context.Users.Any())
            {
                context.Users.AddRange(rep.Users);
                context.SaveChanges();
                context.Posts.AddRange(rep.Posts);
                context.SaveChanges();
                context.Messages.AddRange(rep.Messages);
                context.SaveChanges();
                context.Photos.AddRange(rep.Photos);
                context.SaveChanges();
                context.Comments.AddRange(rep.Comments);
                context.SaveChanges();
                context.Likes.AddRange(rep.Likes);
                context.SaveChanges();
                context.Friendships.AddRange(rep.Friendships);
                context.SaveChanges();
            }
        }
    }
}
