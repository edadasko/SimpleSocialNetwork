using System;
using System.Collections.Generic;
using System.Linq;
using SimpleSocialNetwork.Models;
namespace SimpleSocialNetwork
{
    public class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            User[] users =
            {
                new User
                {
                    Name = "Эдуард",
                    Surname = "Адасько",
                    BirthDay = new DateTime(2000, 3, 30),
                    Country = "Беларусь",
                    City = "Барановичи",
                    Address = "Жукова 18/2 - 24",
                    School = "СШ 19",
                    University = "БГУИР",
                },

                new User
                {
                    Name = "Полина",
                    Surname = "Ушакова",
                    BirthDay = new DateTime(2000, 9, 22),
                    Country = "Беларусь",
                    City = "Минск",
                    Address = "Восточая 26 - 35",
                    School = "СШ 19",
                    University = "БГУИР",
                },

                new User
                {
                    Name = "Дмитрий",
                    Surname = "Ширко",
                    BirthDay = new DateTime(2000, 2, 5),
                    Country = "Беларусь",
                    City = "Барановичи",
                    Address = "Наконечникова",
                    School = "СШ 19",
                    University = "МГЛУ",
                }
            };

   
            Message m1 = new Message
            {
                Date = DateTime.Now,
                Text = "Привет",
                UserFrom = users[0],
                UserTo = users[1]
            };

            Message m2 = new Message
            {
                Date = DateTime.Now,
                Text = "Привет!!!",
                UserFrom = users[1],
                UserTo = users[0]
            };

            Message m3 = new Message
            {
                Date = DateTime.Now,
                Text = "Как дела?",
                UserFrom = users[0],
                UserTo = users[2]
            };

            Post post = new Post
            {
                Date = DateTime.Now,
                Text = "ЛЛАЛАЛАЛАЛАЛАЛА",
                Owner = users[0]
            };

            Photo photo = new Photo
            {
                Image = "img1.jpg",
                Post  = post
            };

            Comment c1 = new Comment
            {
                Date = DateTime.Now,
                Text = "First comment",
                Post = post,
                Owner = users[2],
            };

            Comment c2 = new Comment
            {
                Date = DateTime.Now,
                Text = "Second comment",
                Post = post,
                Owner = users[0],
            };

            Like like1 = new Like
            {
                Owner = users[2],
                Post = post,
            };

            Like like2 = new Like
            {
                Owner = users[0],
                Post = post,
            };

            Like like3 = new Like
            {
                Owner = users[1],
                Post = post,
            };


            Comment[] comments = { c1, c2 };
            Message[] messages = { m1, m2, m3 };
            Like[] likes = { like1, like2, like3 };

            Friendship[] friendships =
            {
                new Friendship()
                {
                    RequestFrom = users[0],
                    RequestTo = users[1],
                    Status = FriendshipStatus.Confirmed
                },

                new Friendship()
                {
                    RequestFrom = users[1],
                    RequestTo = users[2],
                    Status = FriendshipStatus.Confirmed
                },

                new Friendship()
                {
                    RequestFrom = users[2],
                    RequestTo = users[0],
                    Status = FriendshipStatus.Confirmed
                },
            };

            foreach (var entity in context.Users)
                context.Users.Remove(entity);

            foreach (var entity in context.Posts)
                context.Posts.Remove(entity);

            foreach (var entity in context.Likes)
                context.Likes.Remove(entity);

            foreach (var entity in context.Messages)
                context.Messages.Remove(entity);

            foreach (var entity in context.Photos)
                context.Photos.Remove(entity);

            foreach (var entity in context.Comments)
                context.Comments.Remove(entity);

            foreach (var entity in context.Friendships)
                context.Friendships.Remove(entity);

            context.Users.AddRange(users);
            context.SaveChanges();
            context.Posts.Add(post);
            context.SaveChanges();
            context.Messages.AddRange(messages);
            context.SaveChanges();
            context.Photos.Add(photo);
            context.SaveChanges();
            context.Comments.AddRange(comments);
            context.SaveChanges();
            context.Likes.AddRange(likes);
            context.SaveChanges();
            context.Friendships.AddRange(friendships);
            context.SaveChanges();

        }
    }
}
