using BusinessObjects.DTO;
using Microsoft.AspNetCore.Http;
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
    public class UserController : Controller
    {
        private readonly HttpClient client = null;
        private string UserApiUrl = ""; 
        private string RecipesApiUrl = "";
        public UserController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            UserApiUrl = "https://localhost:5001/api/Users";
            RecipesApiUrl = "https://localhost:5001/api/Recipes";
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int Id) {
            return View();
        }
        public async Task<IActionResult> Profile(int id)
        {
            HttpResponseMessage response = await client.GetAsync(UserApiUrl + "/" + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            UserDTO user = JsonSerializer.Deserialize<UserDTO>(strData, options);

            //get recipes by user
            response = await client.GetAsync(RecipesApiUrl + "/user/" + id);
            string strData2 = await response.Content.ReadAsStringAsync();
            List<RecipeDTO> recipes = JsonSerializer.Deserialize<List<RecipeDTO>>(strData2, options);
            ViewBag.Recipes = recipes;
            return View(user);
        }
        public async Task<IActionResult> MyProfile()
        {
            UserDTO user = SessionExtension.Get<UserDTO>(HttpContext.Session, "user");
            if (user != null)
            {
                return RedirectToAction("Profile", new { id = user.UserId });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
