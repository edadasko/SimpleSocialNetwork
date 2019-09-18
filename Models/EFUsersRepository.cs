using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SimpleSocialNetwork.Models
{
    public class EFUsersRepository : IUsersRepository
    {
        private UsersContext context;

        public EFUsersRepository(UsersContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<User> Users => context.Users;
        public IEnumerable<Message> Messages => context.Messages;
        public IEnumerable<Post> Posts => context.Posts;
        public IEnumerable<Like> Likes => context.Likes;
        public IEnumerable<Comment> Comments => context.Comments;
        public IEnumerable<Photo> Photos => context.Photos;
        public IEnumerable<Friendship> Friendships => context.Friendships;


        public List<Post> GetUsersPosts(User user)
        {
            context.Entry(user)
                   .Collection(c => c.Posts)
                   .Load();

            foreach (var post in user.Posts)
            {
                context.Entry(post)
                       .Collection(c => c.Likes)
                       .Load();

                context.Entry(post)
                       .Collection(c => c.Comments)
                       .Load();

                context.Entry(post)
                        .Collection(c => c.Photos)
                        .Load();
            }
            return user.Posts.ToList();
        }

        public List<User> GetUsersFriends(User user)
        {
            context.Entry(user)
                   .Collection(c => c.IncomingFrienshipRequests)
                   .Load();

            context.Entry(user)
                   .Collection(c => c.IncomingFrienshipRequests)
                   .Load();

            return user.Friends;
        }

        public List<Post> GetUsersNews(User user)
        {
            var news = new List<Post>();
            var friends = GetUsersFriends(user);
            foreach (var friend in friends)
            {
                news.AddRange(GetUsersPosts(friend));
            }
            return news.OrderBy(n => n.Date).ToList();
        }

        public Dictionary<User, List<Message>> GetUsersMessages(User user)
        {
            context.Entry(user)
                   .Collection(c => c.MessageFrom)
                   .Load();

            context.Entry(user)
                   .Collection(c => c.MessageTo)
                   .Load();

            return user.Messages;
        }

        public List<Post> GetUsersPhotos(User user)
        {
            context.Entry(user)
                   .Collection(c => c.Posts)
                   .Load();

            var onlyPhotoPosts = user.Photos;
            foreach (var photo in onlyPhotoPosts)
            {
                context.Entry(photo)
                       .Collection(c => c.Likes)
                       .Load();

                context.Entry(photo)
                       .Collection(c => c.Comments)
                       .Load();

                context.Entry(photo)
                        .Collection(c => c.Photos)
                        .Load();

            }
            return onlyPhotoPosts;
        }

        public Post GetUserMainPhoto(User user)
        {
            context.Entry(user)
                    .Collection(c => c.Posts)
                    .Load();

            var mainPhoto = user.MainPhoto;

            context.Entry(mainPhoto)
                   .Collection(c => c.Likes)
                   .Load();

            context.Entry(mainPhoto)
                   .Collection(c => c.Comments)
                   .Load();

            context.Entry(mainPhoto)
                    .Collection(c => c.Photos)
                    .Load();

            return mainPhoto;
        }

        public void ClearData()
        {
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
            context.SaveChanges();
        }

    }
}
