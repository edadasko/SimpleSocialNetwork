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
            return LoadInformationFromPosts(user.Posts.ToList());
        }

        public List<Post> GetUsersPosts(User user, int num)
        {
            var posts = context.Posts.Where(p => p.Owner == user).Take(num).ToList();
            return LoadInformationFromPosts(posts);
        }

        private List<Post> LoadInformationFromPosts(List<Post> posts)
        {
            foreach (var post in posts)
            {
                void load(string x) => context.Entry(post)
                                              .Collection(x)
                                              .Load();
                load("Likes");
                load("Comments");
                load("Photos");

                context.Entry(post)
                              .Reference("Owner")
                              .Load();
            }
            return posts;
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

        public List<User> GetDialogs(User user)
        {
            var users = context.Messages.Where(m => m.UserFrom == user).Select(u => u.UserTo).Distinct().ToList();
            users.AddRange(context.Messages.Where(m => m.UserTo == user).Select(u => u.UserFrom).Distinct().ToList());
            return users;
        }

        public List<Message> GetUsersMessages(User user, User dialogUser)
        {
            var outMessages = context.Messages.Where(m => m.UserFrom == user && m.UserTo == dialogUser).ToList();
            var inMessages = context.Messages.Where(m => m.UserFrom == dialogUser && m.UserTo == user).ToList();
            var messages = outMessages;
            messages.AddRange(inMessages);
            return messages.OrderByDescending(m => m.Date).ToList();
        }

        public Message GetFirstMessage(User user, User userWith)
        {
            var outMessage = context.Messages.Where(m => m.UserFrom == user && m.UserTo == userWith).Last();
            var inMessage = context.Messages.Where(m => m.UserFrom == userWith && m.UserTo == user).Last();
            return outMessage.Date > inMessage.Date ? outMessage : inMessage;
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
            var mainPhoto = context.Posts.Where(p => p.Type == PostType.MainPhoto && p.Owner == user).
                                          Single();

            void load(string x) => context.Entry(mainPhoto)
                                          .Collection(x)
                                          .Load();

            load("Likes");
            load("Comments");
            load("Photos");

            return mainPhoto;
        }

        public void GetUsersMainPageInfo(User user)
        {
            GetUsersFriends(user);
            GetUsersMainPhoto(user);
            GetUsersPhotos(user);
            GetUsersPosts(user);
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

        public void Create(User user)
        {
            var mainPhotoPost = new Post()
            {
                Type = PostType.MainPhoto,
                Owner = user
            };

            var mainPhoto = new Photo()
            {
                Image = "~/images/no_photo.png",
                Post = mainPhotoPost
            };

            context.Posts.Add(mainPhotoPost);
            context.Photos.Add(mainPhoto);
            context.Users.Add(user);
        }

        public void Remove(User user) => context.Users.Remove(user);
        public void Update(User user) => context.Users.Update(user);

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
