using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class RecipesController : Controller
    {
        private readonly HttpClient client = null;
        private string RecipesApiUrl = "";
        private string IngredientsApiUrl = "";
        private string CommentApiUrl = "";

        public RecipesController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            RecipesApiUrl = "https://localhost:5001/api/Recipes";
            IngredientsApiUrl = "https://localhost:5001/api/Ingredients";
            CommentApiUrl = "https://localhost:5001/api/Comments";
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            UserDTO user = SessionExtension.Get<UserDTO>(HttpContext.Session, "user");
            if (user!= null)
            {
                ViewData["userId"] = user.UserId;
            }
            string token = "";
            if (HttpContext.Session.Get("token") != null && HttpContext.Session.Get("user") != null)
            {
                token = HttpContext.Session.GetString("token");
                ViewData["token"] = token;
            }

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
        public async Task<IActionResult> Draft()
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
            HttpResponseMessage response = await client.GetAsync(RecipesApiUrl + "/drafts");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<RecipeDTO> recipes = JsonSerializer.Deserialize<List<RecipeDTO>>(data, options);
                return View(recipes);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
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

            HttpResponseMessage response = await client.GetAsync(RecipesApiUrl + "/" + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            RecipeDTO recipe = JsonSerializer.Deserialize<RecipeDTO>(strData, options);
            if(recipe != null)
            {
                return View(recipe);
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public async Task<JsonResult> UploadImage([FromServices] IHostingEnvironment hostingEnvironment)
        {
            string uniqueFileName = null;
            if (Request.Form.Files.Count != 0)
            {
                var image = Request.Form.Files[0];

                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images/fromUsers");

                uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;

                using (var fs = new FileStream(Path.Combine(uploadsFolder, uniqueFileName), FileMode.Create))
                {
                    await image.CopyToAsync(fs);
                }
                //return Json(image);
            }
            return Json(uniqueFileName);
        }
        
    }
}
