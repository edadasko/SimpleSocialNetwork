using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.Components
{
    public class DialogViewComponent : ViewComponent
    {
        IUsersRepository _repository;
        User _user;

        public DialogViewComponent(IUsersRepository repository,
                                    IHttpContextAccessor httpContextAccessor,
                                    UserManager<User> userManager)
        {
            _repository = repository;
            var id = userManager.GetUserId(httpContextAccessor.HttpContext.User);
            _user = _repository.GetUserById(id);
        }

        public IViewComponentResult Invoke(string userId)
        {
            ViewBag.ownerUser = _user;
            var otherUser = _repository.GetUserById(userId);
            _repository.GetUsersMainPhoto(otherUser);
            ViewBag.otherUser = otherUser;

            Dialog dialog = null;
            try
            {
                dialog = _repository.GetDialogs(_user)
                                    .Single(d => (d.User1Id == _user.Id && d.User2Id == userId) ||
                                                 (d.User2Id == _user.Id && d.User1Id == userId));
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
