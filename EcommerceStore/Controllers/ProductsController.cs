using EcommerceStore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using EcommerceStore.Services;
namespace EcommerceStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IServices _Service;
        public ProductsController(IServices services)
        {
            _Service= services;
        }
        public IActionResult Index()
        {
            var json = HttpContext.Session.GetString("UserObject");
            User user = JsonConvert.DeserializeObject<User>(json);
            if (user.IsAdmin)
                TempData["Isadmin"] = true;
            return View("Products");
        }
        public IActionResult Order([FromForm] Order order)
        {
            var json = HttpContext.Session.GetString("UserObject");
            User user = JsonConvert.DeserializeObject<User>(json);
            _Service.CreateOrder(order,user);
            return RedirectToAction("Index");
        }
    }
}
