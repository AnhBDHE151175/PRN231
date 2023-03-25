using LandingPage.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRN231.DTOs.RequestModels;
using PRN231.DTOs.ResponseModels;
using PRN231_UI.Utils;
using System.Diagnostics;
using System.Text;

namespace LandingPage.Controllers
{
    public class LandingController : Controller
    {
        Uri baseAddress = new Uri(Constants.API_URL);
        HttpClient client;
        public LandingController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public IActionResult Index(int pageIndex = 1)
        {
            Pager request = new Pager()
            {
                PageIndex = pageIndex,
                PageSize = 10,
                Keyword = "",
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + Constants.JOB_GETALL, content).Result;

            var dataString = response.Content.ReadAsStringAsync().Result;
            var dataObject = JsonConvert.DeserializeObject<ListDataOutput<JobResponse>>(dataString);
            if (!dataObject.IsError)
            {
                ViewBag.Data = dataObject.Data;
            }
            return View();
        }
        public IActionResult Insert(IFormCollection form)
        {
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