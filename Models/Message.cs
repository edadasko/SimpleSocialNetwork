using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleSocialNetwork.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int? UserFromId { get; set; }
        public int? UserToId { get; set; }

        [InverseProperty("MessageFrom")]
        public User UserFrom { get; set; }

        [InverseProperty("MessageTo")]
        public User UserTo { get; set; }
        
    }
}
