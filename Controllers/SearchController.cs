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

        public ActionResult SearchResponse(string request, int page)
        {
            request ??= "";
            IEnumerable<User> response = GetResponse(request);
            return PartialView("SearchResult", GetItemsPage(page, response));
        }

        [NonAction]
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

        [NonAction]
        private IEnumerable<User> GetResponse(string request)
        {
            var words = request.Split();
            var comp = StringComparison.OrdinalIgnoreCase;
            var response = (words.Count()) switch
            {
                0 => _repository.Users,
                1 => _repository.Users.Where(u => u.Name.StartsWith(request, comp)
                                            || u.Surname.StartsWith(request, comp)),
                2 => _repository.Users.Where(u => (u.Name.StartsWith(words[0], comp)
                                            && u.Surname.StartsWith(words[1], comp))
                                            || (u.Surname.StartsWith(words[0], comp)
                                            && u.Name.StartsWith(words[1], comp))),
                _ => null,
            };
            return response;
        }
    }
}
