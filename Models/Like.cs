using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleSocialNetwork.Models
{
    public class Like
    {
        public int Id { get; set; }

        public Post Post { get; set; }
        public int? PostId { get; set; }

        public string OwnerId { get; set; }
        public User Owner { get; set; }
    }
}
