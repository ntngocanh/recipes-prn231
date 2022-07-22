using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class RecipesController : Controller
    {
        private readonly HttpClient client = null;
        private string RecipesApiUrl = "";
        private string CommentApiUrl = "";


        public RecipesController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            RecipesApiUrl = "https://localhost:5001/api/Recipes";
            CommentApiUrl = "https://localhost:5001/api/Comments";
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            UserDTO user = SessionExtension.Get<UserDTO>(HttpContext.Session, "user");
            ViewData["userId"] = user.UserId; 
            HttpResponseMessage response = await client.GetAsync(RecipesApiUrl + "/" + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            RecipeDTO recipe = JsonSerializer.Deserialize<RecipeDTO>(strData, options);
            response = await client.GetAsync(CommentApiUrl + "/getByRecipeBase/" + id);
            string strData2 = await response.Content.ReadAsStringAsync();
            List<CommentDTO> comments = JsonSerializer.Deserialize<List<CommentDTO>>(strData2, options);
            ViewBag.Comments = comments;
            return View(recipe);
        }
        //public async Task<IActionResult> Create()
        //{
        //    return View();
        //}
        public async Task<IActionResult> Create()
        {
            Recipe recipe = new Recipe()
            {
                UserId = SessionExtension.Get<UserDTO>(HttpContext.Session, "user").UserId,
                DateCreated = DateTime.Now,
                RecipeStatus = RecipeStatus.Draft
            };
            string token = "";
            if (HttpContext.Session.Get("token") != null)
            {
                token = HttpContext.Session.GetString("token");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var json = JsonSerializer.Serialize(recipe);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(RecipesApiUrl, data);

            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Recipe r = JsonSerializer.Deserialize<Recipe>(result, options);
                return RedirectToAction("Edit",  "Recipes", new { id = r.RecipeId.ToString() });
            }
            else
            {
                return RedirectToAction("Forbiden", "Home");
            }
        }
        public async Task<IActionResult> Edit(int id)
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
