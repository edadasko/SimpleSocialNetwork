﻿using System;
using System.Linq;
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

        public ViewResult Index(int? userWithId = null)
        {
            ViewBag.userWithId = userWithId;
            return View();
        }

        [Route("sendMessage/{userToId}/{text}")]
        public RedirectResult SendMessage(int userToId, string text)
        {
            var userTo = _repository.GetUserById(userToId);
            Dialog dialog;
            try
            {
                dialog = _repository.GetDialogs(_user)
                                    .Single(d => (d.User1Id == _user.UserId && d.User2Id == userToId) ||
                                                 (d.User2Id == _user.UserId && d.User1Id == userToId));

            }
            catch (InvalidOperationException)
            {
                dialog = new Dialog()
                {
                    User1Id = _user.UserId,
                    User2Id = userToId
                };

                _repository.Create(dialog);
                _repository.Save();
            }

            Message message = new Message
            {
                UserFrom = _user,
                UserTo = userTo,
                Text = text,
                Date = DateTime.Now,
                Dialog = dialog
            };

            _repository.Create(message);
            _repository.Save();
            dialog.LastMessageId = _repository.Messages.Single(p => p == message).Id;
            _repository.Update(dialog);
            _repository.Save();
            return Redirect("~/dialog/" + userToId);

        }

        [Route("dialog/{userId}")]
        public ViewComponentResult Dialog(int userId)
        {
            return ViewComponent("Dialog", new { userId });
        }

        [Route("dialogsList")]
        public ViewComponentResult DialogsList()
        {
            return ViewComponent("DialogsList");
        }
    }
}
