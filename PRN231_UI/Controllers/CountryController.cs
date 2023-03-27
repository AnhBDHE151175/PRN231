using Microsoft.AspNetCore.Mvc;
using PRN231.Entities;
using PRN231_UI.Models;
using PRN231_UI.Utils;
using System.Diagnostics;
using System.Text.Json;

namespace PRN231_UI.Controllers
{
    public class CountryController : Controller
    {
        public Uri _baseAddress = new Uri(Constants.API_URL);
        HttpClient _httpClient;

        public CountryController()
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
            List<Country> products = new List<Country>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + $"{Constants.COUNTRY_API}?name={name}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                products = JsonSerializer.Deserialize<List<Country>>(data, options);
            }

            ViewData["key"] = name;
            return View(products);
        }
        public IActionResult Update()
        {
            return Redirect("/Index");
        }
        public IActionResult Create()
        {

            return Redirect("/Index");
        }
        public IActionResult Delete()
        {

            return Redirect("/Index");
        }
    }
}