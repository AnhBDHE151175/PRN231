using Microsoft.AspNetCore.Mvc;
using PRN231_UI.Models;
using System.Diagnostics;

namespace PRN231_UI.Controllers
{
    public class DepartmentController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


    }
}