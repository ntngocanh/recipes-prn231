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
    public class CollectionsController : Controller
    {
        private readonly HttpClient client = null;
        private string RecipesApiUrl = "";
        private string CollectionApiUrl = "";


        public CollectionsController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            RecipesApiUrl = "https://localhost:5001/api/Recipes";
            CollectionApiUrl = "https://localhost:5001/api/Collections";
        }
        public async Task<IActionResult> Index()
        {
            int UserId = SessionExtension.Get<UserDTO>(HttpContext.Session, "user").UserId;
            HttpResponseMessage response = await client.GetAsync(CollectionApiUrl + "/getByUser/" + UserId);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<CollectionDTO> collections = JsonSerializer.Deserialize<List<CollectionDTO>>(strData, options);
            return View(collections);
        }
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await client.GetAsync(CollectionApiUrl + "/" + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            CollectionDTO collection = JsonSerializer.Deserialize<CollectionDTO>(strData, options);
            response = await client.GetAsync(CollectionApiUrl + "/GetRecipes/" + id);
            string strData2 = await response.Content.ReadAsStringAsync();
            List<RecipeDTO> recipes = JsonSerializer.Deserialize<List<RecipeDTO>>(strData2, options);
            ViewBag.Recipes = recipes;
            return View(collection);
        }
        //public async Task<IActionResult> Create()
        //{
        //    return View();
        //}
        public IActionResult Create()
        {
            string token = "";
            if (HttpContext.Session.Get("token") != null && HttpContext.Session.Get("user") != null)
            {
                token = HttpContext.Session.GetString("token");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            UserDTO user = SessionExtension.Get<UserDTO>(HttpContext.Session, "user");
            if (user != null)
            {
                ViewData["userId"] = user.UserId;
                ViewBag.userId = user.UserId;
            }
            else
                ViewData["userId"] = 0;
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            string token = "";
            if (HttpContext.Session.Get("token") != null && HttpContext.Session.Get("user") != null)
            {
                token = HttpContext.Session.GetString("token");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            UserDTO user = SessionExtension.Get<UserDTO>(HttpContext.Session, "user");
            if (user != null)
            {
                ViewData["userId"] = user.UserId;
            }
            else
                ViewData["userId"] = 0;
            HttpResponseMessage response = await client.GetAsync(CollectionApiUrl + "/" + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            CollectionDTO collection = JsonSerializer.Deserialize<CollectionDTO>(strData, options);
            return View(collection);
        }

        public IActionResult MyCollections(int id)
        {
            UserDTO user = SessionExtension.Get<UserDTO>(HttpContext.Session, "user");
            if (user != null)
            {
                ViewData["loggedUserId"] = user.UserId;
            }
            else
                ViewData["loggedUserId"] = 0;

            ViewData["userId"] = id;
            return View();
        }
        public IActionResult Recipes(int id)
        {
            UserDTO user = SessionExtension.Get<UserDTO>(HttpContext.Session, "user");
            if (user != null)
            {
                ViewData["userId"] = user.UserId;
            }
            else
                ViewData["userId"] = 0;
            ViewData["collId"] = id;
            ViewBag.CollId = id;
            return View();
        }
    }
}
