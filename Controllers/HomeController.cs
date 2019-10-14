using Microsoft.AspNetCore.Mvc;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        IUsersRepository _repository;
        public HomeController(IUsersRepository repository)
        {
            _repository = repository;
        }

        public RedirectToActionResult Index()
        {
            //_repository.ClearData();
            return RedirectToAction("Index", "User");
        }
    }
}
