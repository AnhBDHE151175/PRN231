using Microsoft.AspNetCore.Mvc;
using PRN231.Entities;
using PRN231_UI.Utils;
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
        public IActionResult Index(string name, int? pageIndex)
        {
            List<Candidate> candidates = new();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + $"{Constants.CANDIDATE_API}?name={name}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                candidates = JsonSerializer.Deserialize<List<Candidate>>(data, options);
            }
            ViewBag.Current = pageIndex ?? 1;
            ViewBag.TotalPage = (int)Math.Ceiling(candidates.Count() * 1.0 / Constants.PAGE_SIZE);

            candidates = candidates.Skip((pageIndex ?? 1 - 1) * Constants.PAGE_SIZE).Take(Constants.PAGE_SIZE).ToList();
            ViewData["SearchName"] = name;
            return View(candidates);
            
        }

    }
}
