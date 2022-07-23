using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly RecipeDbContext _context;

        public IngredientsController(RecipeDbContext context)
        {
            _context = context;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients(int recipeId)
        {
            return await _context.Ingredients.Where(i => i.RecipeId == recipeId).ToListAsync();
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredient>> GetIngredient(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            return ingredient;
        }


        // POST: api/Ingredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> PostIngredient(Ingredient ingredient)
        {
            var userId = -1;
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                userId = Int32.Parse(identity.FindFirst("Id").Value);
            }
            Recipe recipe = _context.Recipes.FirstOrDefault(r => r.RecipeId == ingredient.RecipeId);
            if(recipe.UserId != userId)
            {
                return Unauthorized();
            }
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();

            return ingredient.IngredientId;
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            var userId = -1;
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                userId = Int32.Parse(identity.FindFirst("Id").Value);
            }
            var ingredient = await _context.Ingredients.Include(i => i.Recipe).FirstOrDefaultAsync(i => i.IngredientId == id);
            if (ingredient == null)
            {
                return NotFound();
            }
            if (ingredient.Recipe.UserId != userId)
            {
                return Unauthorized();
            }
            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientExists(int id)
        {
            return _context.Ingredients.Any(e => e.IngredientId == id);
        }
        [Authorize]
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Ingredient> patchEntity)
        {
            var userId = -1;
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                userId = Int32.Parse(identity.FindFirst("Id").Value);
            }

            var entity = _context.Ingredients.Include(i => i.Recipe).FirstOrDefault(i => i.IngredientId == id);

            if (entity == null)
            {
                return NotFound();
            }
            if (entity.Recipe.UserId != userId)
            {
                return Unauthorized();
            }
            patchEntity.ApplyTo(entity);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
