using System;
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
            return View(users);
        }

        [HttpGet]
        [Route("dialog/{userId}")]
        public PartialViewResult Dialog(int userId)
        {
            ViewBag.ownerUser = _user;
            var otherUser = _repository.GetUserById(userId);
            ViewBag.otherUser = otherUser;
            _repository.GetUsersMainPhoto(otherUser);
            Dialog dialog = null;
            try
            {
                dialog = _repository.GetDialogs(_user)
                                    .Single(d => (d.User1Id == _user.UserId && d.User2Id == userId) ||
                                                 (d.User2Id == _user.UserId && d.User1Id == userId));
            }
            catch (InvalidOperationException)
            {
                return PartialView(new List<Message>());
            }

            var messages =_repository.GetMessagesFromDialog(dialog);
            return PartialView(messages);
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
                    User2Id = userToId,
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

            return Redirect("~/dialog/" + userToId);

        }
    }
}