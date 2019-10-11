using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.Components
{
    public class DialogViewComponent : ViewComponent
    {
        IUsersRepository _repository;
        User _user;
        public DialogViewComponent(IUsersRepository repository)
        {
            _repository = repository;
            _user = ((List<User>)_repository.Users)[0];
        }

        public IViewComponentResult Invoke(int userId)
        {
            ViewBag.ownerUser = _user;
            var otherUser = _repository.GetUserById(userId);
            _repository.GetUsersMainPhoto(otherUser);
            ViewBag.otherUser = otherUser;

            Dialog dialog = null;
            try
            {
                dialog = _repository.GetDialogs(_user)
                                    .Single(d => (d.User1Id == _user.UserId && d.User2Id == userId) ||
                                                 (d.User2Id == _user.UserId && d.User1Id == userId));
            }
            catch (InvalidOperationException)
            {
                return View(new List<Message>());
            }

            var messages = _repository.GetMessagesFromDialog(dialog);
            return View(messages);
        }
    }
}
