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

        public IEnumerable<User> Users => context.Users.ToList();
        public IEnumerable<Message> Messages => context.Messages.ToList();
        public IEnumerable<Post> Posts => context.Posts.ToList();
        public IEnumerable<Like> Likes => context.Likes.ToHashSet();
        public IEnumerable<Comment> Comments => context.Comments.ToList();
        public IEnumerable<Photo> Photos => context.Photos.ToList();
        public IEnumerable<Friendship> Friendships => context.Friendships.ToList();


        public List<Post> GetUsersPosts(User user)
        {
            context.Entry(user)
                   .Collection(c => c.Posts)
                   .Load();

            foreach (var post in user.Posts)
            {
                void load(string x) => context.Entry(post)
                                              .Collection(x)
                                              .Load();
                load("Likes");
                load("Comments");
                load("Photos");
            }
            return user.Posts.ToList();
        }

        public List<User> GetUsersFriends(User user)
        {
            void loadRequests(string x) => context.Entry(user)
                                                  .Collection(x)
                                                  .Load();
            loadRequests("IncomingFrienshipRequests");
            loadRequests("OutgoingFrienshipRequests");

            void loadSenders(string x, Friendship request)
                            => context.Entry(request)
                                      .Reference(x)
                                      .Load();

            foreach (var request in user.IncomingFrienshipRequests)
                loadSenders("RequestFrom", request);

            foreach (var request in user.OutgoingFrienshipRequests)
                loadSenders("RequestTo", request);
            
            return user.Friends;
        }

        public List<Post> GetUsersNews(User user)
        {
            var news = new List<Post>();
            var friends = GetUsersFriends(user);
            foreach (var friend in friends)
                news.AddRange(GetUsersPosts(friend));
            return news.OrderBy(n => n.Date).ToList();
        }

        public Dictionary<User, List<Message>> GetUsersMessages(User user)
        {
            void loadMessages(string x) => context.Entry(user)
                                                  .Collection(x)
                                                  .Load();

            loadMessages("MessageFrom");
            loadMessages("MessageTo");

            var messagesFrom = user.MessageFrom;
            var messageTo = user.MessageTo;

            void loadSenders(string x, Message message)
                            => context.Entry(message)
                                      .Reference(x)
                                      .Load();

            foreach (var message in messagesFrom)
                loadSenders("UserTo", message);

            foreach (var message in messageTo)
                loadSenders("UserFrom", message);

            return user.Messages;
        }

        public List<Post> GetUsersPhotos(User user)
        {
            context.Entry(user)
                   .Collection(c => c.Posts)
                   .Load();

            void load(string x, Post photo)
                            => context.Entry(photo)
                                      .Collection(x)
                                      .Load();

            var onlyPhotoPosts = user.Photos;
            foreach (var photo in onlyPhotoPosts)
            {
                load("Likes", photo);
                load("Comments", photo);
                load("Photos", photo);
            }
            return onlyPhotoPosts;
        }

        public Post GetUsersMainPhoto(User user)
        {
            context.Entry(user)
                    .Collection(c => c.Posts)
                    .Load();

            var mainPhoto = user.MainPhoto;

            void load(string x) => context.Entry(mainPhoto)
                                          .Collection(x)
                                          .Load();

            load("Likes");
            load("Comments");
            load("Photos");

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

        public void Create(Message message) => context.Messages.Add(message);
        public void Create(Post post) => context.Posts.Add(post);
        public void Remove(Post post) => context.Posts.Remove(post);
        public void Create(Like like) => context.Likes.Add(like);
        public void Remove(Like like) => context.Likes.Remove(like);
        public void Create(Comment comment) => context.Comments.Add(comment);
        public void Remove(Comment comment) => context.Comments.Remove(comment);
        public void Create(Photo photo) => context.Photos.Add(photo);
        public void Remove(Photo photo) => context.Photos.Remove(photo);
        public void Create(Friendship friendship) => context.Friendships.Add(friendship);
        public void Update(Friendship friendship) => context.Friendships.Update(friendship);
        public void Remove(Friendship friendship) => context.Friendships.Remove(friendship);
        public void Save() => context.SaveChanges();

        public User GetUserById(int id) => context.Users.Find(id);
    }
}
