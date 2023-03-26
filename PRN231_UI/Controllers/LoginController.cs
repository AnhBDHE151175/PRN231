using Microsoft.AspNetCore.Mvc;
using PRN231_UI.Models;
using System.Diagnostics;

namespace PRN231_UI.Controllers
{
    public class LoginController : Controller
    {


        public IActionResult Index()
        {
            TempData["IsLogin"] = false;
            return View();
        }
        public IActionResult Login()
        {
            HttpContext.Session.SetString("IsLogin", "true");

            TempData["IsLogin"] = true;
            return Redirect("/Home/Index");
        }


    }
}