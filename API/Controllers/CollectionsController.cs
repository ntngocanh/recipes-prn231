using API.DTO;
using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly RecipeDbContext _context;
        private MapperConfiguration config;
        private IMapper mapper;

        public CollectionsController(RecipeDbContext context)
        {
            _context = context;
            config = new MapperConfiguration(cf => cf.AddProfile(new MapperProfile()));
            mapper = config.CreateMapper();
        }

//Get All Collections of user

        [HttpGet("getByUser/{userId}")]
        public IActionResult GetByUser(int userId)
        {
            if (_context.Users.FirstOrDefault(x => x.UserId == userId) == null)
            {
                return NotFound();
            }
            List<Collection> collections = _context.Collections.Where(x => x.UserId == userId).ToList();
            if (collections == null)
            {
                return NotFound();
            }
            List<CollectionDTO> collectionDTOs = collections.Select(m => mapper.Map<Collection, CollectionDTO>(m)).ToList();
            foreach (CollectionDTO c in collectionDTOs)
            {
                //string image = _context.Recipes.FirstOrDefault(x => x.RecipeId == _context.CollectionRecipes
                //                          .FirstOrDefault(x => x.CollectionId == c.CollectionId).RecipeId)
                //                          .Image;
                //if (image!=null)
                //    c.Image = image;
                c.NumberOfRecipes = CountRecipes(c.CollectionId);
            }
            return Ok(collectionDTOs);
        }

//Get All Collections of user without recipe

        [HttpGet("getByUser/{userId}/{recipeId}")]
        public IActionResult GetByUser(int userId, int recipeId)
        {
            if (_context.Users.FirstOrDefault(x => x.UserId == userId) == null)
            {
                return NotFound();
            }
            List<Collection> collections = _context.Collections.Where(x => x.UserId == userId).ToList();
            if (collections == null)
            {
                return NotFound();
            }
            List<CollectionDTO> collectionDTOs = collections.Select(m => mapper.Map<Collection, CollectionDTO>(m)).ToList();
            List<CollectionDTO> collectionToDelete = new List<CollectionDTO>();
            foreach (CollectionDTO c in collectionDTOs)
            {
                CollectionRecipe cr = _context.CollectionRecipes.FirstOrDefault(x => x.CollectionId == c.CollectionId && x.RecipeId == recipeId);
                if (cr != null)
                    collectionToDelete.Add(c);
                //string image = _context.Recipes.FirstOrDefault(x => x.RecipeId == _context.CollectionRecipes
                //                          .FirstOrDefault(x => x.CollectionId == c.CollectionId).RecipeId)
                //                          .Image;
                //if (image!=null)
                //    c.Image = image;
                c.NumberOfRecipes = CountRecipes(c.CollectionId);
            }
            foreach (var c in collectionToDelete)
            {
                collectionDTOs.Remove(c);
            }
            return Ok(collectionDTOs);
        }

//Get All Recipes of Collection

        [HttpGet("GetRecipes/{collectionId}")]
        public IActionResult GetRecipesByCollection(int collectionId)
        {
            if (_context.Collections.FirstOrDefault(x => x.CollectionId == collectionId) == null)
            {
                return NotFound();
            }

            List<CollectionRecipe> collectionRecipes = _context.CollectionRecipes.Where(x => x.CollectionId == collectionId).ToList();
            if (collectionRecipes == null)
            {
                return NotFound();
            }
            List<Recipe> recipes = new List<Recipe>();
            foreach (var item in collectionRecipes)
            {
                recipes.Add(_context.Recipes.FirstOrDefault(x => x.RecipeId == item.RecipeId));
            }
            List<RecipeDTO> recipeDTOs = recipes.Select(m => mapper.Map<Recipe, RecipeDTO>(m)).ToList();
            return Ok(recipeDTOs);
        }

//Get Collection

        [HttpGet("{collectionId}")]
        public IActionResult GetCollection(int collectionId)
        {
            Collection c = _context.Collections.FirstOrDefault(x => x.CollectionId == collectionId);
            if (c == null)
            {
                return NotFound();
            }
            CollectionDTO collectionDTO = mapper.Map<Collection, CollectionDTO>(c);
            collectionDTO.NumberOfRecipes = CountRecipes(collectionId);
            return Ok(collectionDTO);
        }

//Edit Collection

        [HttpPut("{collectionId}")]
        public async Task<IActionResult> PutCollection(int collectionId, Collection collection)
        {
            if (collectionId != collection.CollectionId)
            {
                return BadRequest();
            }

            _context.Entry(collection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectionExists(collectionId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

//Add Collection

        [HttpPost]
        public async Task<ActionResult<Collection>> PostCollection(Collection collection)
        {
            _context.Collections.Add(collection);
            await _context.SaveChangesAsync();

            return collection;
        }

        //Delete Collection

        [HttpDelete("{collectionId}")]
        public async Task<IActionResult> DeleteCollection(int collectionId)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection == null)
            {
                return NotFound();
            }

            _context.Collections.Remove(collection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Delete CollectionRecipe

        [HttpDelete("DeleteCR")]
        public async Task<IActionResult> DeleteCollectionRecipe(CollectionRecipe cr)
        {
            if (cr == null)
            {
                return NotFound();
            }

            _context.CollectionRecipes.Remove(cr);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool CollectionExists(int id)
        {
            return _context.Collections.Any(e => e.CollectionId == id);
        }
        private int CountRecipes(int collectionId)
        {
            return _context.CollectionRecipes.Where(x => x.CollectionId == collectionId).ToList().Count;
        }
    }
}
