using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using BusinessObjects.DTO;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RecipeDbContext _context;
        private MapperConfiguration config;
        private IMapper mapper;
        public UsersController(RecipeDbContext context)
        {
            _context = context;
            config = new MapperConfiguration(cf => cf.AddProfile(new MapperProfile()));
            mapper = config.CreateMapper();
        }
        //Get All Users
        [HttpGet]
        public ActionResult<List<User>> GetAllUser() {
            return _context.Users.Include("Role").ToList();
        }
        [HttpGet("Statistic")]
        public ActionResult GetStatisticUser() {
            int numOfUser;
            int numOfAdmin;
            int numOfPremium;
            int numOfNormal;
            var ListUser = _context.Users.ToList();
            if (ListUser == null)
            {
                numOfUser = 0;
            }
            else {
                numOfUser = ListUser.Count;
            }
            var ListAdmin = _context.Users.Where(x=>x.RoleId==1).ToList();
            if (ListAdmin == null)
            {
                numOfAdmin = 0;
            }
            else
            {
                numOfAdmin = ListAdmin.Count;
            }
            var ListPre= _context.Users.Where(x => x.RoleId == 3).ToList();
            if (ListPre == null)
            {
                numOfPremium = 0;
            }
            else
            {
                numOfPremium = ListPre.Count;
            }
            var ListNormal = _context.Users.Where(x => x.RoleId == 2).ToList();
            if (ListNormal == null)
            {
                numOfNormal = 0;
            }
            else
            {
                numOfNormal = ListNormal.Count;
            }
            if (numOfUser != 0) {
                PercentageOfUser admin = new PercentageOfUser("Admin", (float)numOfAdmin / (float)numOfUser *100);
                PercentageOfUser pre = new PercentageOfUser("Premium", (float)numOfPremium / (float)numOfUser *100);
                PercentageOfUser nor = new PercentageOfUser("Normal", (float)numOfNormal / (float)numOfUser *100);
                List<PercentageOfUser> result = new List<PercentageOfUser>();
                result.Add(admin);
                result.Add(pre);
                result.Add(nor);
                return Ok(result);
            }
            else {
                PercentageOfUser admin = new PercentageOfUser("Admin", 0);
                PercentageOfUser pre = new PercentageOfUser("Premium", 0);
                PercentageOfUser nor = new PercentageOfUser("Normal", 0);
                List<PercentageOfUser> result = new List<PercentageOfUser>();
                result.Add(admin);
                result.Add(pre);
                result.Add(nor);
                return Ok(result);
            }
        }
        //Get Premium Users
        [HttpGet]
        [Route("Premium")]
        public ActionResult<List<User>> GetAllPremiumUser()
        {
            return _context.Users.Where(x=>x.RoleId==3).ToList();
        }

        [HttpPut]
        [Route("Premium/{Id}")]
        public ActionResult UpgradeToPremium(int Id) {
            var u = _context.Users.FirstOrDefault(x => x.UserId == Id);
            if (u == null) {
                return NotFound();
            }
            if (u.RoleId == 3) {
                return AcceptedAtAction("User is already premium!");
            }
            u.RoleId = 3;
            _context.SaveChanges();
            return Ok("Upgrade Successfully!");
        }

        [HttpPut]
        [Route("RequestVIP/{Id}")]
        public ActionResult Request(int Id)
        {
            var u = _context.Users.FirstOrDefault(x => x.UserId == Id);
            if (u == null)
            {
                return NotFound();
            }
            if (u.RoleId == 3)
            {
                return AcceptedAtAction("User is already premium!");
            }
            u.RequestToVIP = true;
            _context.SaveChanges();
            return Ok("Request Successfully!");
        }
        [HttpPut]
        [Route("EditProfile/{Id}")]
        public ActionResult EditProfile(int Id,User user)
        {
            var u = _context.Users.FirstOrDefault(x => x.UserId == Id);
            if (u == null)
            {
                return NotFound();
            }
            u.Name = user.Name;
            u.RoleId = user.RoleId;
            u.Avatar = user.Avatar;
            _context.SaveChanges();
            return Ok();
        }


        [HttpPost("check")]
        public ActionResult<User> CheckExisted(string email)
        {
            return _context.Users.SingleOrDefault(a => a.Email == email); 
        }
        [HttpPost]
        public ActionResult<User> Register(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id) { 
            var user = _context.Users.Include("Role").SingleOrDefault(a => a.UserId == id);
            if (user == null)
                return NotFound();
            else {
                User u = new User();
                u.UserId = user.UserId;
                u.Email = user.Email;
                u.Name = user.Name;
                u.Role = user.Role;
                u.Avatar = user.Avatar;
                return Ok(u);
            }
        }

        // GET: api/Recipes/5
        [HttpGet("profile/{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(r => r.UserId == id);

            if (user == null)
            {
                return NotFound();
            }
            UserDTO userDTO = mapper.Map<User, UserDTO>(user);
            return userDTO;
        }
    }
}
