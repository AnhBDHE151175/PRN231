using Microsoft.AspNetCore.Mvc;
using PRN231_UI.Models;
using System.Diagnostics;

namespace PRN231_UI.Controllers
{
    public class EmployeeController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Update()
        {

            return View();
        }
    }
}