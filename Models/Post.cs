using System;
using System.Collections.Generic;

public enum PostType
{
    Normal,
    PhotoOnly,
    MainPhoto
}

namespace SimpleSocialNetwork.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string OwnerId { get; set; }

        public PostType Type { get; set; }

        public ICollection<Photo> Photos { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public User Owner { get; set; }

        public Post()
        {
            Photos = new List<Photo>();
            Likes = new HashSet<Like>();
            Comments = new List<Comment>();
        }

        public override string ToString() => Owner + "\n" +
                                             Date.ToString() + "\n" + Text + "\n"
                                             + string.Join("\n", Photos) +
                                             "\nКоличество лайков: " + Likes.Count +
                                             "\nКомментарии: \n" + string.Join("\n", Comments);
    }
}
