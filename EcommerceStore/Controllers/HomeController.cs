using EcommerceStore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcommerceStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var json = HttpContext.Session.GetString("UserObject");
            User user = JsonConvert.DeserializeObject<User>(json);
            if (user.IsAdmin)
                TempData["Isadmin"] = true;
            return View();
        }
    }
}
