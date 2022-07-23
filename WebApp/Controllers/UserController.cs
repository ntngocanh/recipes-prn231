using BusinessObjects.DTO;
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
    public class UserController : Controller
    {
        private readonly HttpClient client = null;
        private string TokenApiUrl = "";
        private string UsersApiUrl = "";
        public UserController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

           
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int Id) {
            return View();
        }

       [HttpPut]
        public async Task<IActionResult> RequestVIPAsync() {

            var url = "https://localhost:5001/api/Users/RequestVIP" + SessionExtension.Get<UserDTO>(HttpContext.Session, "user").UserId;
            HttpResponseMessage response = await client.PutAsync("https://localhost:5001/api/Users/RequestVIP/"+ SessionExtension.Get<UserDTO>(HttpContext.Session,"user").UserId,null);

            if (response.IsSuccessStatusCode)
            {
                return Json("Successfully");
                SessionExtension.Get<UserDTO>(HttpContext.Session, "user").RequestToVIP = false;

            }
            else return Json("Request fail!");
        }
    }
}
