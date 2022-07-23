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

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
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

    }
}
