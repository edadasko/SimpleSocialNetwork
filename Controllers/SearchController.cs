using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.Controllers
{
    public class SearchController : Controller
    {
        IUsersRepository _repository;
        const int pageSize = 5;

        public SearchController(IUsersRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View(GetItemsPage(0, GetResponse("")));
        }

        private List<User> GetItemsPage(int page, IEnumerable<User> response)
        {
            if (response == null || !response.Any())
                return null;
            var itemsToSkip = page * pageSize;
            var users = response.Skip(itemsToSkip).Take(pageSize).ToList();
            foreach (var user in users)
                _repository.GetUsersMainPhoto(user);
            return users;
        }

        public ActionResult SearchResponse(string request, int page)
        {
            if (request == null)
                request = "";
            IEnumerable<User> response = GetResponse(request);
            return PartialView("SearchResult", GetItemsPage(page, response));
        }

        [NonAction]
        private IEnumerable<User> GetResponse(string request)
        {
            IEnumerable<User> response;
            var words = request.Split();
            switch (words.Count())
            {
                case 0:
                    response = _repository.Users;
                    break;
                case 1:
                    response = _repository.Users.Where(u => u.Name.StartsWith(request, StringComparison.OrdinalIgnoreCase)
                                                            || u.Surname.StartsWith(request, StringComparison.OrdinalIgnoreCase));
                    break;
                case 2:
                    response = _repository.Users.Where(u => (u.Name.StartsWith(words[0], StringComparison.OrdinalIgnoreCase)
                                                             && u.Surname.StartsWith(words[1], StringComparison.OrdinalIgnoreCase))
                                                            || (u.Surname.StartsWith(words[0], StringComparison.OrdinalIgnoreCase)
                                                             && u.Name.StartsWith(words[1], StringComparison.OrdinalIgnoreCase)));
                    break;
                default:
                    response = null;
                    break;
            }
            return response;
        }
    }
}
