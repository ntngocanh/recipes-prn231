using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient client = null;
        private string TokenApiUrl = "";
        private string UsersApiUrl = "";
        public AuthController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            TokenApiUrl = "https://localhost:5001/api/Token";
            UsersApiUrl = "https://localhost:5001/api/Users";
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel user)
        {

            var json = JsonSerializer.Serialize(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(TokenApiUrl, data);

            if (response.IsSuccessStatusCode)
            {
                string mess = await response.Content.ReadAsStringAsync();
                string token = JsonSerializer.Deserialize<string>(mess);

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token);
                var tokenS = jsonToken as JwtSecurityToken;


                var userClaims = tokenS.Claims;
                UserDTO u = new UserDTO();
                u.Email = userClaims.FirstOrDefault(o => o.Type == "Email")?.Value;
                u.UserId = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == "UserId")?.Value);
                u.Name = userClaims.FirstOrDefault(o => o.Type == "Name")?.Value;
                u.Avatar = userClaims.FirstOrDefault(o => o.Type == "Avatar")?.Value;
                u.RoleName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value;
                u.RequestToVIP = Convert.ToBoolean( userClaims.FirstOrDefault(o => o.Type == "RequestToVIP")?.Value);
                SessionExtension.Set<UserDTO>(HttpContext.Session, "user", u);
                HttpContext.Session.SetString("token", token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Wrong email or password");
                return View(user);
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            HttpContext.Session.Remove("token");
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                //check email existed
               
                var json = JsonSerializer.Serialize(model.Email);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(UsersApiUrl + "/check", data);

                var result = await response.Content.ReadAsStringAsync();
                if (result != null && result.Length > 0)
                {

                    ModelState.AddModelError(string.Empty, "Email already existed");
                    return View(model);
                }
                //post user
                var user = new User
                {
                    Email = model.Email,
                    Password = CreateMD5(model.Password),
                    Name = model.Name,
                    RoleId = 2,
                    Avatar = "avatar.png"
                };
                json = JsonSerializer.Serialize(user);
                data = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.PostAsync(UsersApiUrl, data);

                result = await response.Content.ReadAsStringAsync();
                //login
                return RedirectToAction("Login", "Auth");

            }
            return View(model);
        }

        private string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                // StringBuilder sb = new System.Text.StringBuilder();
                // for (int i = 0; i < hashBytes.Length; i++)
                // {
                //     sb.Append(hashBytes[i].ToString("X2"));
                // }
                // return sb.ToString();
            }
        }


    }
}
