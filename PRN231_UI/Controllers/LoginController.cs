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
    public class LoginController : Controller
    {
        Uri baseAddress = new Uri(Constants.API_URL);
        HttpClient client;
        public LoginController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            ViewData["message"] = TempData["message"];
            ViewData["fail"] = TempData["fail"];
            return View();
        }
        public IActionResult ForgotPass()
        {
            return View();
        }
        public IActionResult Forgot(string email)
        {
            return View();
        }

        public IActionResult Login(string email, string passWord)
        {
            LoginRequest request = new LoginRequest()
            {
                email = email,
                password = passWord
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + Constants.LOGIN, content).Result;

            var dataString = response.Content.ReadAsStringAsync().Result;
            var dataObject = JsonConvert.DeserializeObject<LoginResponse>(dataString);
            HttpContext.Session.SetString("FullName", dataObject.FullName);
            ViewBag.Data = dataObject;
            if (!string.IsNullOrEmpty(dataObject.FullName))
            {
                return Redirect("/Home/Index");
            }
            TempData["fail"] = "Email or Password incorrect!";
            return Redirect("/login/Index");

        }




    }
}