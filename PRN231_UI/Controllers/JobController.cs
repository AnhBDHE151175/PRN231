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

        public IActionResult Index(int pageIndex = 1, string search = "")
        {
            ViewData["FullName"] = HttpContext.Session.GetString("FullName");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("FullName")))
            {
                TempData["message"] = "Please Login!!!";
                return Redirect("/login/index");
            }
            Pager request = new Pager()
            {
                PageIndex = pageIndex,
                PageSize = 10,
                Keyword = search,
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + Constants.JOB_GETALL, content).Result;

            var dataString = response.Content.ReadAsStringAsync().Result;
            var dataObject = JsonConvert.DeserializeObject<ListDataOutput<JobResponse>>(dataString);
            if (!dataObject.IsError)
            {
                ViewBag.Data = dataObject.Data;
                ViewBag.Keyword = search;
            }
            return View();
        }
        public IActionResult Create()
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

        public IActionResult Update(int id = 0)
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
            //Update
            if (id == 0)
            {
                return Redirect("/job/create");
            }
            HttpResponseMessage responseGet = client.GetAsync(client.BaseAddress + Constants.JOB_GETBYID + $"/{id}").Result;

            var dataStringGet = responseGet.Content.ReadAsStringAsync().Result;
            var dataObjectGet = JsonConvert.DeserializeObject<DataOutput<JobByIdResponse>>(dataStringGet);
            ViewBag.DateString = dataObjectGet.Data.ExpiredDate.Value.Date.ToString("yyyy-MM-dd");
            ViewBag.DataGetById = dataObjectGet.Data;




            return View();
        }
        public IActionResult Delete(int id)
        {

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + Constants.JOB_DELETE + $"/{id}").Result;


            var dataString = response.Content.ReadAsStringAsync().Result;
            var dataObject = JsonConvert.DeserializeObject<Response>(dataString);
            ViewBag.Data = dataObject;

            return Redirect("/Job/index");
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
        [HttpPost]
        public IActionResult Edit(IFormCollection form)
        {

            JobRequest request = new JobRequest()
            {
                job = new Job()
                {
                    Id = Int32.Parse(form["id"].ToString()),
                    JobTitle = form["jobTitle"].ToString(),
                    MinSalary = Decimal.Parse(form["min"].ToString()),
                    MaxSalary = Decimal.Parse(form["max"].ToString()),
                    ExpiredDate = DateTime.Parse(form["expiredDate"].ToString()),
                },
                listSkills = form["skills"].ToString()
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + Constants.JOB_UPDATE, content).Result;

            var dataString = response.Content.ReadAsStringAsync().Result;
            var dataObject = JsonConvert.DeserializeObject<Response>(dataString);

            return Redirect("/job/index");
        }

    }
}