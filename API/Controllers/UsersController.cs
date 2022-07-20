using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RecipeDbContext _context;
        public UsersController(RecipeDbContext context)
        {
            _context = context;
        }
        [HttpPost("check")]
        public ActionResult<User> CheckExisted(string email)
        {
            return _context.Users.SingleOrDefault(a => a.Email == email); ;
        }
        [HttpPost]
        public ActionResult<User> Register(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
