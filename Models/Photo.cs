using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleSocialNetwork.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Image { get; set; }

        public int? PostId { get; set; }
        public Post Post { get; set; }
    }
}
