using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using API.DTO;
using BusinessObjects.Models;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper.QueryableExtensions;
using System.Security.Claims;
using API.Utils;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly RecipeDbContext _context;
        private MapperConfiguration config;
        private IMapper mapper;
        public RecipesController(RecipeDbContext context)
        {
            _context = context;
            config = new MapperConfiguration(cf => cf.AddProfile(new MapperProfile()));
            mapper = config.CreateMapper();
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return await _context.Recipes.ToListAsync();
        }

        [HttpGet("HomePage")]
        public ActionResult<IEnumerable<Recipe>> GetRecipesHomePage()
        {
            var list=  _context.Recipes.OrderByDescending(x=>x.DateCreated).Include(x=>x.User).ToList();
           
            if (list.Count <=4 ) {
                return list;
            }
            List<Recipe> smallList = new List<Recipe>();
            for (int i = 0; i < 4; i++) {
                smallList.Add(list[i]);
                
            }
            return smallList;
        }

        [HttpGet("HomePage/Premium")]
        public ActionResult<IEnumerable<Recipe>> GetRecipesPremium()
        {
            var list = _context.Recipes.OrderByDescending(x => x.DateCreated).Include(x => x.User).Where(x=>x.User.RoleId==3).ToList();

            if (list.Count <= 4)
            {
                return list;
            }
            List<Recipe> smallList = new List<Recipe>();
            for (int i = 0; i < 4; i++)
            {
                smallList.Add(list[i]);

            }
            return smallList;
        }
        [HttpGet("Statistic")]

        public ActionResult GetRecipesByPast7Day()
        {
           
            List<NumberOfRecipePerDay> list = new List<NumberOfRecipePerDay>();
            for (int i = 6; i >= 0; i--) {
                var num = _context.Recipes.Where(x => EF.Functions.DateDiffDay(x.DateCreated,DateTime.Now) == i).AsEnumerable().ToList();
                int number;
                if (num == null) number = 0;
                else number = num.Count;
                list.Add(new NumberOfRecipePerDay(Convert.ToDateTime(DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd")),number));
            }
            return Ok(list);
          
        }
        [HttpGet("Statistic/{startDate}/{endDate}")]

        public ActionResult GetRecipesByDays(DateTime startDate,DateTime endDate)
        {
            DateTime date = startDate;
            List<NumberOfRecipePerDay> list = new List<NumberOfRecipePerDay>();
            foreach (DateTime day in EachDay(startDate, endDate)) {
                var num = _context.Recipes.Where(x => x.DateCreated == day).ToList();
                int number;
                if (num == null) number = 0;
                else number = num.Count;
                list.Add(new NumberOfRecipePerDay(Convert.ToDateTime(day.ToString("yyyy-MM-dd")), number));
            }
              
            return Ok(list);

        }
        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDTO>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Steps)
                .FirstOrDefaultAsync(r => r.RecipeId == id);

            if (recipe == null)
            {
                return NotFound();
            }
            RecipeDTO recipeDTO = mapper.Map<Recipe, RecipeDTO>(recipe);
            return recipeDTO;
        }


        // POST: api/Recipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            var userId = -1;
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                userId = Int32.Parse(identity.FindFirst("Id").Value);
            }
            if(recipe.UserId != userId)
            {
                return Unauthorized();
            }
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return recipe;
        }

        //Add Recipe to Collection

        [HttpPost("addToCollection")]
        public async Task<ActionResult<Recipe>> PostToCollection(CollectionRecipe cr)
        {
            _context.CollectionRecipes.Add(cr);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            var comments = _context.Comments.Where(c => c.RecipeId == recipe.RecipeId);
            var reactions = _context.Reactions.Where(c => c.RecipeId == recipe.RecipeId);
            _context.Reactions.RemoveRange(reactions);
            _context.Comments.RemoveRange(comments);
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("HomePage/Trend")]
        public IActionResult GetTrend() {
           var user= _context.Recipes
                         .GroupBy(a => a.UserId)
                         .Select(g => new { g.Key, Count = g.Count() ,User = _context.Users.FirstOrDefault(x=>x.UserId==g.Key)}).ToList();
            if (user.Count <= 4)
                return Ok(user);
            else {
                var small = user.ToList();
                small.Clear();
                for (int i = 0; i < 4; i++) {
                    small.Add(user[i]);
                }
                return Ok(small);
            }
        }
        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.RecipeId == id);
        }
        [Authorize]
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Recipe> patchEntity)
        {
            var userId = -1;
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                userId = Int32.Parse(identity.FindFirst("Id").Value);
            }

            var entity = _context.Recipes.FirstOrDefault(r => r.RecipeId == id);

            if (entity == null)
            {
                return NotFound();
            }
            if(entity.UserId != userId)
            {
                return Unauthorized();
            }
            patchEntity.ApplyTo(entity);
            _context.SaveChanges();

            return Ok(entity);
        }

        // GET: api/Recipes
        [Authorize]
        [HttpGet("drafts")]
        public async Task<ActionResult<IEnumerable<RecipeDTO>>> GetDraftRecipes()
        {
            var uId = -1;
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                uId = Int32.Parse(identity.FindFirst("Id").Value);
            }
            return await _context.Recipes.Where(r => r.UserId == uId && r.RecipeStatus == RecipeStatus.Draft).ProjectTo<RecipeDTO>(config).ToListAsync();
        }
        [Authorize]
        [HttpGet("edit/{recipeId}")]
        public async Task<ActionResult<RecipeDTO>> GetRecipeToEdit(int recipeId)
        {
            var uId = -1;
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                uId = Int32.Parse(identity.FindFirst("Id").Value);
            }
            var recipe = await _context.Recipes
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Steps)
                .FirstOrDefaultAsync(r => r.RecipeId == recipeId && r.UserId == uId);

            if (recipe == null)
            {
                return NotFound();
            }
            RecipeDTO recipeDTO = mapper.Map<Recipe, RecipeDTO>(recipe);
            return recipeDTO;
        }

        private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
        // recipes by a user
        [Authorize]
        [HttpGet("myrecipes")]
        public async Task<ActionResult<IEnumerable<RecipeDTO>>> GetMyRecipes()
        {
            var uId = -1;
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                uId = Int32.Parse(identity.FindFirst("Id").Value);
            }
            return await _context.Recipes.Where(r => r.UserId == uId).ProjectTo<RecipeDTO>(config).ToListAsync();
        }
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<RecipeDTO>>> GetRecipesByUserId(int userId)
        {
            return await _context.Recipes.Where(r => r.UserId == userId && r.RecipeStatus == RecipeStatus.Published).ProjectTo<RecipeDTO>(config).ToListAsync();
        }
        [HttpGet("search")]
        public IActionResult SearchRecipes([FromQuery] RecipeSearchParameters parameters)
        {
            if (parameters.SearchString == null)
            {
                parameters.SearchString = "";
            }
            if (!parameters.ValidYearRange)
            {
                return BadRequest("Max date cannot be less than min date");
            }
            var configuration = new MapperConfiguration(cf => cf.AddProfile(new RecipeProfile()));

            IQueryable<Recipe> recipesList = _context.Recipes.Include(x => x.User).Include(x => x.Reactions).Where(x => x.Name.Contains(parameters.SearchString));
            List<RecipeDTO> recipeDTOs = recipesList.ProjectTo<RecipeDTO>(configuration).ToList();
            recipeDTOs.Sort((x, y) => y.Popularity.CompareTo(x.Popularity));
            var recipes = PagedList<RecipeDTO>.ToPagedList(recipeDTOs.AsQueryable(), parameters.PageNumber, parameters.PageSize);
            var metadata = new
            {
                recipes.TotalCount,
                recipes.PageSize,
                recipes.CurrentPage,
                recipes.TotalPages,
                recipes.HasNext,
                recipes.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
            return Ok(recipes);
        }

        
    }
}
