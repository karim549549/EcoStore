using Microsoft.AspNetCore.Mvc;
using EcommerceStore.Services;
using Microsoft.Data.SqlClient;
using EcommerceStore.Models;
using Newtonsoft.Json;

namespace EcommerceStore.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServices _Services;
        public LoginController(IServices services) { 
            _Services = services;
        }

        public IActionResult Index()
        {
            return View("Login");
        }
        public IActionResult Signup()
        {
            return RedirectToAction("Index", "Signup");
        }
        public IActionResult Login([FromForm] User user)
        {
            User Found= _Services.Search(user);
            if (Found.Username != null)
            {
                var json = JsonConvert.SerializeObject(Found);
                HttpContext.Session.SetString("UserObject", json);
                return RedirectToAction("Index", "Home");
            }
            TempData["ErrorMessage"] = false;
            return RedirectToAction("Index");
        }

    }
}
