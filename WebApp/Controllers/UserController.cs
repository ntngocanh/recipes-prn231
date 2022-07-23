using BusinessObjects.DTO;
using Microsoft.AspNetCore.Http;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient client = null;
        private string UserApiUrl = ""; 
        private string RecipesApiUrl = "";
        private string CollectionApiUrl = "";
        public UserController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            UserApiUrl = "https://localhost:5001/api/Users";
            RecipesApiUrl = "https://localhost:5001/api/Recipes";
            CollectionApiUrl = "https://localhost:5001/api/Collections";
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
        [HttpGet("user/myprofile/myrecipes")]
        public async Task<IActionResult> MyRecipes()
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
            UserDTO currentUser = SessionExtension.Get<UserDTO>(HttpContext.Session, "user");
            if (currentUser != null)
            {
                HttpResponseMessage response = await client.GetAsync(UserApiUrl + "/" + currentUser.UserId);
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                UserDTO user = JsonSerializer.Deserialize<UserDTO>(strData, options);

                //get recipes by user
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                response = await client.GetAsync(RecipesApiUrl + "/myrecipes");
                string strData2 = await response.Content.ReadAsStringAsync();
                List<RecipeDTO> recipes = JsonSerializer.Deserialize<List<RecipeDTO>>(strData2, options);
                ViewBag.Recipes = recipes;
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet("user/myprofile/mycollections")]
        public async Task<IActionResult> MyCollections()
        {
            UserDTO currentUser = SessionExtension.Get<UserDTO>(HttpContext.Session, "user");
            if (currentUser != null)
            {
                HttpResponseMessage response = await client.GetAsync(UserApiUrl + "/" + currentUser.UserId);
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                UserDTO user = JsonSerializer.Deserialize<UserDTO>(strData, options);

                //get recipes by user
                response = await client.GetAsync(CollectionApiUrl + "/user/" + currentUser.UserId);
                string strData2 = await response.Content.ReadAsStringAsync();
                List<CollectionDTO> collections = JsonSerializer.Deserialize<List<CollectionDTO>>(strData2, options);
                ViewBag.Collections = collections;
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPut]
        public async Task<IActionResult> RequestVIPAsync() {

            var url = "https://localhost:5001/api/Users/RequestVIP" + SessionExtension.Get<UserDTO>(HttpContext.Session, "user").UserId;
            HttpResponseMessage response = await client.PutAsync("https://localhost:5001/api/Users/RequestVIP/"+ SessionExtension.Get<UserDTO>(HttpContext.Session,"user").UserId,null);

            if (response.IsSuccessStatusCode)
            {
                var userDTO = SessionExtension.Get<UserDTO>(HttpContext.Session, "user");
                userDTO.RequestToVIP = true;
                SessionExtension.Set<UserDTO>(HttpContext.Session, "user", userDTO);
                return Json("Successfully");
                

            }
            else return Json("Request fail!");
        }
    }
}
