using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRN231.Entities;
using PRN231_UI.Models;
using PRN231_UI.Utils;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace PRN231_UI.Controllers
{
    public class RegionController : Controller
    {
        public Uri _baseAddress = new Uri(Constants.API_URL);
        HttpClient _httpClient;

        public RegionController()
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
            List<Region> regions = new();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + $"{Constants.REGION_API}?name={name}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                regions = System.Text.Json.JsonSerializer.Deserialize<List<Region>>(data, options);
            }

            return View(regions);
        }
        public IActionResult Update(IFormCollection form)
        {
            var request = new Region
            {
                Id = Int32.Parse(form["region-id"]),
                RegionName = form["region-name"].ToString().Trim()
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + $"{Constants.REGION_API}", content).Result;

            var data = response.Content.ReadAsStringAsync().Result;
            return Redirect("/Region");
        }
        public IActionResult Create(IFormCollection form)
        {
            var request = new Region
            {
                RegionName = form["region-name"].ToString().Trim()
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + $"{Constants.REGION_API}", content ).Result;

            var data = response.Content.ReadAsStringAsync().Result;
            return Redirect("/Region");
        }
        public IActionResult Delete(int id)
        {
            var content = new Dictionary<string, string>
            {
                { "id", id.ToString()}
            };
            HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + $"{Constants.REGION_API}/{id}").Result;
            return Redirect("/Region");
        }
    }
}