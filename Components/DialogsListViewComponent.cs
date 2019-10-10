using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.Components
{
    public class DialogsListViewComponent : ViewComponent
    {
        IUsersRepository _repository;
        User _user;
        public DialogsListViewComponent(IUsersRepository repository)
        {
            _repository = repository;
            _user = ((List<User>)_repository.Users)[0];
        }

        public IViewComponentResult Invoke()
        {
            var dialogs = _repository.GetDialogs(_user);
            var users = new List<(User, Message)>();
            foreach (var dialog in dialogs)
            {
                var second_user = dialog.User1Id == _user.UserId ?
                            _repository.GetUserById(dialog.User2Id) :
                            _repository.GetUserById(dialog.User1Id);
                _repository.GetUsersMainPhoto(second_user);
                users.Add((second_user, _repository.GetMessageById(dialog.LastMessageId)));
            }
            users = users.OrderByDescending(u => u.Item2.Date).ToList();
            return View(users);
        }
    }
}
