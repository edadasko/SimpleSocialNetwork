﻿using System;
using System.Collections.Generic;

namespace SimpleSocialNetwork.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }

        public Post Post { get; set; }
        public int? PostId { get; set; }

        public string OwnerId { get; set; }
        public User Owner { get; set; }

        public override string ToString() => Date.ToString() + "\n" + Owner + ": \n" + Text + "\n";
    }
}
