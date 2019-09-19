using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.Controllers
{
    public class MessageController : Controller
    {
        IUsersRepository _repository;
        User _user;
        public MessageController(IUsersRepository repository)
        {
            _repository = repository;
            _user = ((List<User>)_repository.Users)[0];
        }

        public string Index()
        {
            var messages =_repository.GetUsersMessages(_user);

            string strMessages = "";
            foreach (var user in messages.Keys)
            {
                foreach (var message in messages[user])
                {
                    strMessages += message.ToString();
                }
                strMessages += "\n";
            }

            return strMessages;
        }

        public string SendMessage(int idTo, string text)
        {
            var userTo = _repository.GetUserById(idTo);

            if (userTo == null)
                return Index();

            Message message = new Message
            {
                UserFrom = _user,
                UserTo = userTo,
                Text = text,
                Date = DateTime.Now
            };

            _repository.Create(message);
            _repository.Save();
            return Index();
        }
    }
}