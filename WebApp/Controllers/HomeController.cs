using BusinessObjects.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient client = null;
        private string RecipeApiUrl = "";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            RecipeApiUrl = "https://localhost:5001/api/Recipes";
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDTO>>> Search(string searchQuery, int pageNumber, DateTime? fromDate, DateTime? toDate)
        {
            var MinDate = DateTime.Now.AddYears(-1);
            if (fromDate != null)
            {
                MinDate = (DateTime)fromDate;
            }
            var MaxDate = DateTime.Now.AddYears(1);
            if (toDate != null)
            {
                MinDate = (DateTime)toDate;
            }
            var request = new Dictionary<string, string>
            {
                { "SearchString", searchQuery },
                { "pageNumber", "1" },
                { "pageSize", "15" },
                { "MinDate", MinDate.ToString("yyyy-MM-dd") },
                { "MaxDate", MaxDate.ToString("yyyy-MM-dd") },

            };
            var dictFormUrlEncoded = new FormUrlEncodedContent(request);
            var queryString = await dictFormUrlEncoded.ReadAsStringAsync();
            var response = await client.GetAsync(RecipeApiUrl + "/search?" + queryString);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
            List<RecipeDTO> recipeDTOs = JsonSerializer.Deserialize<List<RecipeDTO>>(result, options);
            ViewBag.SearchString = searchQuery;
            ViewBag.FromDate = MinDate.ToString("yyyy-MM-dd");
            ViewBag.ToDate = MaxDate.ToString("yyyy-MM-dd");

            return View(recipeDTOs);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
