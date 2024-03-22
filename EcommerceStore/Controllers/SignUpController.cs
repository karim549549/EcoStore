using Microsoft.AspNetCore.Mvc;
using EcommerceStore.Models;
using EcommerceStore.Services;
namespace EcommerceStore.Controllers
{
    public class SignUpController : Controller
    {
        private readonly IServices _Services;
        public SignUpController(IServices Services)
        {
            _Services = Services;
        }
        public IActionResult Index()
        {
            return View("SignUp");
        }
        public IActionResult Login()
        {
            return RedirectToAction("Index","Login");
        }
        [HttpPost]
        public  IActionResult CreateUser([FromForm] User user)
        {
            User Found= _Services.Search(user);
            if (Found.Username != null)
            {
                TempData["ErrorMessage"] = true;
                return RedirectToAction("Index");
            }
            _Services.CreateUser(user);
            return RedirectToAction("Index", "Login");
        }
    }
}
