using Microsoft.AspNetCore.Mvc;
using PRN231.DTOs.RequestModels;
using PRN231.DTOs.ResponseModels;
using PRN231.Entities;
using PRN231_UI.Utils;
using System.Text;
using System.Text.Json;

namespace PRN231_UI.Controllers
{
    public class CandidateController : Controller
    {
        public Uri _baseAddress = new Uri(Constants.API_URL);
        HttpClient _httpClient;

        public CandidateController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseAddress;
        }
        public IActionResult Index(string name, int? pageIndex, int? deId = 0)
        {
            ViewData["FullName"] = HttpContext.Session.GetString("FullName");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("FullName")))
            {
                TempData["message"] = "Please Login!!!";
                return Redirect("/login/index");
            }
            List<Candidate> candidates = new();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + $"{Constants.CANDIDATE_API}?name={name}&departmentId={deId}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                candidates = JsonSerializer.Deserialize<List<Candidate>>(data, options);
            }
            List<Department> products = new();
            HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + $"{Constants.DEPARTMENT_API}").Result;

            if (res.IsSuccessStatusCode)
            {
                string data2 = res.Content.ReadAsStringAsync().Result;

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                products = JsonSerializer.Deserialize<List<Department>>(data2, options);
                ViewBag.Departments = products;

            }




            ViewBag.Current = pageIndex ?? 1;
            ViewBag.TotalPage = (int)Math.Ceiling(candidates.Count() * 1.0 / Constants.PAGE_SIZE);

            candidates = candidates.Skip((pageIndex ?? 1 - 1) * Constants.PAGE_SIZE).Take(Constants.PAGE_SIZE).ToList();
            ViewData["SearchName"] = name;
            ViewBag.DeID = deId;
            return View(candidates);

        }
        public IActionResult SendMail(string mail)
        {
            ViewBag.Mail = mail;
            return View();
        }
        public IActionResult Send(IFormCollection form)
        {
            MailRequest request = new MailRequest()
            {
                ToEmail= form["email"].ToString(),
                Subject= form["subject"].ToString(),
                Message= form["message"].ToString(),
            };

            StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + Constants.SEND, content).Result;

            var dataString = response.Content.ReadAsStringAsync().Result;
            var dataObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(dataString);
            return Redirect("/candidate/index");
        }
    }
}
