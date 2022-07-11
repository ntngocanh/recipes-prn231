using BusinessObjects.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class RecipesController : Controller
    {
        private readonly HttpClient client = null;
        private string RecipesApiUrl = "";
        public RecipesController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            RecipesApiUrl = "https://localhost:5001/api/Recipes";
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await client.GetAsync(RecipesApiUrl + "/" + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            RecipeDTO recipe = JsonSerializer.Deserialize<RecipeDTO>(strData, options);
            return View(recipe);
        }
    }
}
