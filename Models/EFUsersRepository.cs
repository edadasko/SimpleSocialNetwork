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
            return news.OrderBy(n => n.Date).Reverse().ToList();
        }

        public List<Dialog> GetDialogs(User user)
        {
            var q = context.Dialogs.ToList();
            var dialogs = context.Dialogs.Where(d => d.User1Id == user.UserId || d.User2Id == user.UserId).ToList();
            return dialogs;
        }

        public List<Message> GetMessagesFromDialog(Dialog dialog)
        {
            context.Entry(dialog).Collection("Messages").Load();
            return dialog.Messages.ToList();
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
            foreach (var friend in user.Friends)
                GetUsersMainPhoto(friend);
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
            foreach (var entity in context.Dialogs)
                context.Dialogs.Remove(entity);
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
        public Post GetPostById(int id) => context.Posts.Find(id);
        public Like GetLikeById(int id) => context.Likes.Find(id);
        public Comment GetCommentById(int id) => context.Comments.Find(id);
    }
}
