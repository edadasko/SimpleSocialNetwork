using System;
using Microsoft.AspNetCore.Http;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork
{
    public class UserInfoViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDay { get; set; }
        public Gender Gender { get; set; }

        public string Email { get; set; }
        public string MobilePhone { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public string School { get; set; }
        public string University { get; set; }

        public string JobPlace { get; set; }
        public string JobPosition { get; set; }

        public IFormFile Avatar { get; set; }
    }
}
