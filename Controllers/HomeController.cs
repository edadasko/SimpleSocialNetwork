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
            //_repository.ClearData();
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "User");
            return View();
        }

        public ActionResult NoAccess()
        {
            return View();
        }
    }
}
