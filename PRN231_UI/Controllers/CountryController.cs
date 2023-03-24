using Microsoft.AspNetCore.Mvc;
using PRN231.Entities;
using PRN231_UI.Models;
using System.Diagnostics;

namespace PRN231_UI.Controllers
{
    public class CountryController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}