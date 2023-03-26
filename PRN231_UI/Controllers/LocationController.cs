using Microsoft.AspNetCore.Mvc;
using PRN231.Entities;
using PRN231_UI.Models;
using PRN231_UI.Utils;
using System.Diagnostics;
using System.Text.Json;

namespace PRN231_UI.Controllers
{
    public class LocationController : Controller
    {

        public Uri _baseAddress = new Uri(Constants.API_URL);
        HttpClient _httpClient;

        public LocationController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseAddress;
        }
        public IActionResult Index(string name)
        {
            ViewData["FullName"] = HttpContext.Session.GetString("FullName");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("FullName")))
            {
                TempData["message"] = "Please Login!!!";
                return Redirect("/login/index");
            }
            List<Location> products = new();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + $"{Constants.LOCATION_API}?name={name}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                products = JsonSerializer.Deserialize<List<Location>>(data, options);
            }

            return View(products);
        }
        public IActionResult Update()
        {

            return View();
        }

    }
}