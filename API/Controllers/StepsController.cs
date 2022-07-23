using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StepsController : ControllerBase
    {
        private readonly RecipeDbContext _context;

        public StepsController(RecipeDbContext context)
        {
            _context = context;
        }

        // GET: api/Steps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Step>>> GetSteps()
        {
            return await _context.Steps.ToListAsync();
        }

        // GET: api/Steps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Step>> GetStep(int id)
        {
            var step = await _context.Steps.FindAsync(id);

            if (step == null)
            {
                return NotFound();
            }

            return step;
        }

        // POST: api/Steps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> PostStep(Step step)
        {
            var userId = -1;
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                userId = Int32.Parse(identity.FindFirst("Id").Value);
            }
            Recipe recipe = _context.Recipes.FirstOrDefault(r => r.RecipeId == step.RecipeId);
            if (recipe.UserId != userId)
            {
                return Unauthorized();
            }
            _context.Steps.Add(step);
            await _context.SaveChangesAsync();

            return step.StepId;
        }

        // DELETE: api/Steps/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStep(int id)
        {
            var userId = -1;
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                userId = Int32.Parse(identity.FindFirst("Id").Value);
            }
            var step = await _context.Steps.Include(i => i.Recipe).FirstOrDefaultAsync(i => i.StepId == id);
            if (step == null)
            {
                return NotFound();
            }
            if (step.Recipe.UserId != userId)
            {
                return Unauthorized();
            }
            _context.Steps.Remove(step);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StepExists(int id)
        {
            return _context.Steps.Any(e => e.StepId == id);
        }
        [Authorize]
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Step> patchEntity)
        {
            var userId = -1;
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                userId = Int32.Parse(identity.FindFirst("Id").Value);
            }

            var entity = _context.Steps.Include(i => i.Recipe).FirstOrDefault(i => i.StepId == id);

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
