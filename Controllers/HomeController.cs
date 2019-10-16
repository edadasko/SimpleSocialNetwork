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

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "User");
            //_repository.ClearData();
            return View();
        }
    }
}
