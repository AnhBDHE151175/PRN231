using LandingPage.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRN231.DTOs.RequestModels;
using PRN231.DTOs.ResponseModels;
using PRN231.Entities;
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
            ViewBag.Data = dataObject.Data;


            //Candidate request2 = new Candidate()
            //{
            //    FirstName = form["firstName"].ToString(),
            //    LastName = form["lastName"].ToString(),
            //    PhoneNumber = form["phone"].ToString(),
            //    Email = form["email"].ToString(),
            //    JobId = Int32.Parse(form["jobId"].ToString()),
            //    Salary = Decimal.Parse(form["salary"].ToString()),
            //};

            //StringContent content2 = new StringContent(JsonConvert.SerializeObject(request2), Encoding.UTF8, "application/json");
            HttpResponseMessage response2 = client.GetAsync(client.BaseAddress + Constants.DEPARTMENT_API).Result;

            var dataString2 = response2.Content.ReadAsStringAsync().Result;
            var dataObject2 = JsonConvert.DeserializeObject<List<Department>>(dataString2);
            ViewBag.DataDepartment = dataObject2;
            return View();
        }
        public IActionResult Insert(IFormCollection form)
        {
            try
            {
                Candidate request = new Candidate()
                {
                    FirstName = form["firstName"].ToString(),
                    LastName = form["lastName"].ToString(),
                    PhoneNumber = form["phone"].ToString(),
                    Email = form["email"].ToString(),
                    JobId = Int32.Parse(form["jobId"].ToString()),
                    Salary = Decimal.Parse(form["salary"].ToString()),
                    DepartmentId = Int32.Parse(form["departmentId"].ToString()),
                };

                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Candidates", content).Result;

                var dataString = response.Content.ReadAsStringAsync().Result;
                var dataObject = JsonConvert.DeserializeObject<bool>(dataString);

                return Redirect("/landing/thanks");
            }
            catch (Exception ex)
            {
                return Redirect("/landing/index");
            }

        }

        public IActionResult Thanks()
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