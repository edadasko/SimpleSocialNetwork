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
            var dialogs = _repository.GetDialogs(_user);
            var users = new List<User>();
            foreach (var dialog in dialogs)
            {
                var second_user = dialog.User1Id == _user.UserId ?
                            _repository.GetUserById(dialog.User2Id) :
                            _repository.GetUserById(dialog.User1Id);
                _repository.GetUsersMainPhoto(second_user);
                users.Add(second_user);
            }
            return string.Join("\n", users);
        }

        public string Dialog()
        {
            var messages =_repository.GetMessagesFromDialog(_repository.GetDialogs(_user)[0]);
            return string.Join("\n", messages);
        }
        public string SendMessage(User userTo, string text)
        {
            // сначала проверить существование диалога, получить или создать новый, затем создать сообщение 
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