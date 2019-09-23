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
            // вывести список диалогов
            throw new NotImplementedException();
        }

        public string Dialog()
        {
            // вывести сообщения с конктретным юзером
            throw new NotImplementedException();
        }
        public string SendMessage(User userTo, string text)
        {
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