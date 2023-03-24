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
    public class JobController : Controller
    {
        Uri baseAddress = new Uri(Constants.API_URL);
        HttpClient client;
        public JobController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            Pager request = new Pager()
            {
                PageIndex = 0,
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


    }
}