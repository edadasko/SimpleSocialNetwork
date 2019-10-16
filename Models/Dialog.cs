using System;
using System.Collections.Generic;

namespace SimpleSocialNetwork.Models
{
    public class Dialog
    {
        public int Id { get; set; }

        public string User1Id { get; set; }
        public string User2Id { get; set; }

        public int LastMessageId { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
