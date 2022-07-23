using API.DTO;
using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
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
    
    public class ReactionsController : ControllerBase
    {
        private readonly RecipeDbContext _context;
        private MapperConfiguration config;
        private IMapper mapper;
        public ReactionsController(RecipeDbContext context)
        {
            _context = context;
            config = new MapperConfiguration(cf => cf.AddProfile(new MapperProfile()));
            mapper = config.CreateMapper();
        }

        [HttpGet("getByRecipe/{recipeId}")]
        public IActionResult GetReactionsOfRecipe(int recipeId)
        {
            List<Reaction> reactions = _context.Reactions.Include(x => x.User).Where(x => x.RecipeId == recipeId).ToList();
            if (reactions == null)
            {
                return NotFound();
            }
            List<ReactionDTO> commentDTOs = reactions.Select(m => mapper.Map<Reaction, ReactionDTO>(m)).ToList();

            return Ok(commentDTOs);
        }

        [HttpGet("getByUserAndRecipe/{recipeId}/{userId}")]
        public IActionResult GetReactionsByRecipeAndType(int recipeId, int userId)
        {
            Reaction reaction = _context.Reactions.Include(x => x.User).FirstOrDefault(x => x.RecipeId == recipeId && x.UserId == userId);
            if (reaction == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Reaction, ReactionDTO>(reaction));
        }

        [HttpPost]
        [Authorize]

        public IActionResult PostReaction(ReactionRequestDTO reaction)
        {
            try
            {

                _context.Reactions.Add(mapper.Map<ReactionRequestDTO, Reaction>(reaction));
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Authorize]

        public IActionResult DeleteReaction(ReactionRequestDTO reaction)
        {
            try
            {
                Reaction r = _context.Reactions.FirstOrDefault(x => x.RecipeId == reaction.RecipeId && x.UserId == reaction.UserId);
                if (r == null)
                {
                    return NotFound();
                }
                _context.Reactions.Remove(r);
                _context.SaveChanges();
                return Ok();
            } catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Authorize]

        public IActionResult EditReaction(ReactionRequestDTO reaction)
        {
            try
            {
                Reaction r = _context.Reactions.FirstOrDefault(x => x.RecipeId == reaction.RecipeId && x.UserId == reaction.UserId);
                if (r == null)
                {
                    return NotFound();
                }
                r.ReactionType = reaction.ReactionType;
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
