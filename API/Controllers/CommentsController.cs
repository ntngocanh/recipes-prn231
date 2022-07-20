using API.DTO;
using AutoMapper;
using BusinessObjects.DTO;
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
    public class CommentsController : ControllerBase
    {
        private readonly RecipeDbContext _context;
        private MapperConfiguration config;
        private IMapper mapper;

        public CommentsController(RecipeDbContext context)
        {
            _context = context;
            config = new MapperConfiguration(cf => cf.AddProfile(new MapperProfile()));
            mapper = config.CreateMapper();
        }

        [HttpGet("getByRecipe/{recipeId}")]
        public IActionResult GetByRecipe(int recipeId)
        {
            if (_context.Recipes.FirstOrDefault(x => x.RecipeId == recipeId) == null)
            {
                return NotFound();
            }

            List<Comment> comments = _context.Comments.Include(x => x.User).Where(x => x.RecipeId == recipeId).ToList();
            if (comments == null)
            {
                return NotFound();
            }
            List<CommentDTO> commentDTOs = comments.Select(m => mapper.Map<Comment, CommentDTO>(m)).ToList();
            foreach (CommentDTO c in commentDTOs)
            {
                c.NumberOfReplies = CountReplies(c.CommentId);
            }
            return Ok(commentDTOs);
        }

        [HttpGet("getByRecipeBase/{recipeId}")]
        public IActionResult GetByRecipeBaseLevel(int recipeId)
        {
            if (_context.Recipes.FirstOrDefault(x => x.RecipeId == recipeId) == null)
            {
                return NotFound();
            }

            List<Comment> comments = _context.Comments.Include(x => x.User).Where(x => x.RecipeId == recipeId && x.ParentCommentId == null && x.CommentStatus== CommentStatus.Posted).ToList();
            if (comments == null)
            {
                return NotFound();
            }
            List<CommentDTO> commentDTOs = comments.Select(m => mapper.Map<Comment, CommentDTO>(m)).ToList();
            foreach (CommentDTO c in commentDTOs)
            {
                c.NumberOfReplies = CountReplies(c.CommentId);
            }
            return Ok(commentDTOs);
        }
        [HttpGet("getByComment/{commentId}")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentReplies(int commentId)
        {
            if (_context.Comments.FirstOrDefault(x => x.CommentId == commentId) == null)
            {
                return NotFound();
            }

            List<Comment> comments = _context.Comments.Include(x => x.User).Where(x => x.ParentCommentId == commentId && x.CommentStatus == CommentStatus.Posted).ToList();
            if (comments == null)
            {
                return NotFound();
            }
            List<CommentDTO> commentDTOs = comments.Select(m => mapper.Map<Comment, CommentDTO>(m)).ToList();
            foreach (CommentDTO c in commentDTOs)
            {
                c.NumberOfReplies = CountReplies(c.CommentId);
            }
            return Ok(commentDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(CommentRequest comment)
        {
            /*try
            {*/
            Comment c = mapper.Map<CommentRequest, Comment>(comment);
                _context.Comments.Add(c);
                await _context.SaveChangesAsync();
                return Ok();
           /* } catch (Exception)
            {
                return BadRequest();
            }*/

        }

        [HttpPut]
        public IActionResult EditComment(Comment comment)
        {
            try
            {
                Comment c = _context.Comments.FirstOrDefault(x => x.CommentId == comment.CommentId);
                if (c == null)
                {
                    return NotFound();
                }
                c.Text = comment.Text;
                _context.SaveChanges();
                return Ok();
            } 
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{commentId}")]
        public IActionResult DeleteComment(int commentId)
        {
            try
            {
                Comment c = _context.Comments.FirstOrDefault(x => x.CommentId == commentId);
                if (c == null)
                {
                    return NotFound();
                }
                _context.Comments.Remove(c);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        private int CountReplies(int commentId)
        {
            return _context.Comments.Where(x => x.ParentCommentId == commentId).ToList().Count;
        }
    }
}
