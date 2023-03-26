using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRN231.DTOs.RequestModels;
using PRN231.DTOs.ResponseModels;
using PRN231_UI.Models;
using PRN231_UI.Utils;
using System.Diagnostics;
using System.Text;

namespace PRN231_UI.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri(Constants.API_URL);
        HttpClient client;
        public HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public IActionResult Index(int pageIndex = 1)
        {
            ViewData["FullName"] = HttpContext.Session.GetString("FullName");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("FullName")))
            {
                TempData["message"] = "Please Login!!!";
                return Redirect("/login/index");
            }

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + Constants.JOB_ANALY).Result;

            var dataString = response.Content.ReadAsStringAsync().Result;
            var dataObject = JsonConvert.DeserializeObject<AnalysisResponse>(dataString);
            ViewBag.Data = dataObject;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}