using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRN231.DTOs.RequestModels;
using PRN231.DTOs.ResponseModels;
using PRN231.Entities;
using PRN231_UI.Models;
using PRN231_UI.Utils;
using System.Diagnostics;
using System.Text;

namespace PRN231_UI.Controllers
{
    public class JobController : Controller
    {
        Uri baseAddress = new Uri(Constants.API_URL);
        HttpClient client;
        public JobController()
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

        public IActionResult Update()
        {

            Pager request = new Pager()
            {
                PageIndex = 1,
                PageSize = 1000,
                Keyword = "",
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + Constants.SKILL_GETALL, content).Result;

            var dataString = response.Content.ReadAsStringAsync().Result;
            var dataObject = JsonConvert.DeserializeObject<ListDataOutput<Skill>>(dataString);
            if (!dataObject.IsError)
            {
                ViewBag.Data = dataObject.Data;
            }
            return View();
        }
        [HttpPost]
        public IActionResult Insert(IFormCollection form)
        {

            JobRequest request = new JobRequest()
            {
                job = new Job()
                {
                    JobTitle = form["jobTitle"].ToString(),
                    MinSalary = Decimal.Parse(form["min"].ToString()),
                    MaxSalary = Decimal.Parse(form["max"].ToString()),
                    ExpiredDate = DateTime.Parse(form["expiredDate"].ToString()),
                },
                listSkills = form["skills"].ToString()
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + Constants.JOB_INSERT, content).Result;

            var dataString = response.Content.ReadAsStringAsync().Result;
            var dataObject = JsonConvert.DeserializeObject<Response>(dataString);

            return Redirect("/job/index");
        }
    }
}