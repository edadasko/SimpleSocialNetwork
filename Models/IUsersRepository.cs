using System;
using System.Collections.Generic;

namespace SimpleSocialNetwork.Models
{
    public interface IUsersRepository
    {
        IEnumerable<User> Users { get; }
        IEnumerable<Post> Posts { get; }
        IEnumerable<Like> Likes { get; }
        IEnumerable<Comment> Comments { get; }
        IEnumerable<Message> Messages { get; }
        IEnumerable<Photo> Photos { get; }
        IEnumerable<Friendship> Friendships { get; }

        List<Post> GetUsersPosts(User user);
        List<Post> GetUsersPhotos(User user); 
        List<User> GetUsersFriends(User user);
        List<Post> GetUsersNews(User user);
        Dictionary<User, List<Message>> GetUsersMessages(User user);
        Post GetUserMainPhoto(User user);
    }
}
