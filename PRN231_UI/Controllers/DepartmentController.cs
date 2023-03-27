using Microsoft.AspNetCore.Mvc;
using PRN231.Entities;
using PRN231_UI.Models;
using PRN231_UI.Utils;
using System.Diagnostics;
using System.Text.Json;

namespace PRN231_UI.Controllers
{
    public class DepartmentController : Controller
    {

        public Uri _baseAddress = new Uri(Constants.API_URL);
        HttpClient _httpClient;

        public DepartmentController()
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
            List<Department> departments = new();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + $"{Constants.DEPARTMENT_API}?name={name}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                departments = JsonSerializer.Deserialize<List<Department>>(data, options);
            }
            ViewBag.TotalPage = (int) Math.Ceiling(departments.Count() * 1.0 / Constants.PAGE_SIZE);
            @ViewData["key"] = name;
            return View(departments);
        }

        public IActionResult Update()
        {

            return View();
        }
    }
}