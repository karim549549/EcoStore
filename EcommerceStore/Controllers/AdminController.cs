using EcommerceStore.Models;
using EcommerceStore.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcommerceStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly IServices _services;

        public AdminController(IServices services)
        {
            _services = services;
        }
        public IActionResult Index()
        {
            var json = HttpContext.Session.GetString("UserObject");
            User user = JsonConvert.DeserializeObject<User>(json);
            if (user.IsAdmin)
                TempData["Isadmin"] = true;
            var orders = _services.GetOrders();
            return View("Admin", orders);
        }
    }
}
