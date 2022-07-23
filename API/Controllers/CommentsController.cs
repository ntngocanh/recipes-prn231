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
        [HttpGet("Report")]
        public IActionResult GetReported()
        {
            List<Report> reports = _context.Reports.Include(x=>x.Comment).ThenInclude(x=>x.User).ToList();
          /*  List<Comment> comments = _context.Comments.Include(x => x.User).Where(x => x.CommentStatus == CommentStatus.Reported).ToList();
            if (comments == null)
            {
                return NotFound();
            }
            List<CommentDTO> commentDTOs = comments.Select(m => mapper.Map<Comment, CommentDTO>(m)).ToList();
            foreach (CommentDTO c in commentDTOs)
            {
                c.NumberOfReplies = CountReplies(c.CommentId);
            }*/
            return Ok(reports);
        }
        [HttpGet("getByRecipeBase/{recipeId}")]
        public IActionResult GetByRecipeBaseLevel(int recipeId)
        {
            if (_context.Recipes.FirstOrDefault(x => x.RecipeId == recipeId) == null)
            {
                return NotFound();
            }

            List<Comment> comments = _context.Comments.Include(x => x.User).Where(x => x.RecipeId == recipeId && x.ParentCommentId == null && x.CommentStatus != CommentStatus.Hidden).ToList();
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
        public ActionResult<IEnumerable<CommentDTO>> GetCommentReplies(int commentId)
        {
            if (_context.Comments.FirstOrDefault(x => x.CommentId == commentId) == null)
            {
                return NotFound();
            }

            List<Comment> comments = _context.Comments.Include(x => x.User).Where(x => x.ParentCommentId == commentId && x.CommentStatus != CommentStatus.Hidden).ToList();
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
        [Authorize]
        public async Task<IActionResult> PostComment(CommentRequest comment)
        {
            /*try
            {*/

            Comment c = mapper.Map<CommentRequest, Comment>(comment);
            if (comment.ParentCommentId == 0)
            {
                c.ParentComment = null;
            }
            _context.Comments.Add(c);
            await _context.SaveChangesAsync();
            int id = c.CommentId;
            Comment c1 = _context.Comments.Include(x => x.User).FirstOrDefault(x => x.CommentId == id);
            CommentDTO commentDTO = mapper.Map<Comment, CommentDTO>(c1);
            return Ok(commentDTO);
            /*}
            catch (Exception)
            {
                return BadRequest();
            }*/

        }
        [HttpPost("reportComment/{commentId}")]

        public async Task<IActionResult> ReportComment(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            //Comment c1 = _context.Comments.Include(x => x.User).FirstOrDefault(x => x.CommentId == report.CommentId);
            //c1.CommentStatus = CommentStatus.Reported;
            //_context.Comments.Update(c1);
            //await _context.SaveChangesAsync();
            return Ok();

        }
        [HttpPut("{commentId}")]
        [Authorize]
        public IActionResult EditComment(CommentRequest comment, int commentId)
        {
            try
            {
                Comment c = _context.Comments.FirstOrDefault(x => x.CommentId == commentId);
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
        [Authorize]

        public IActionResult DeleteComment(int commentId)
        {
            try
            {
                Comment c = _context.Comments.FirstOrDefault(x => x.CommentId == commentId);
                if (c == null)
                {
                    return NotFound();
                }
                c.CommentStatus = CommentStatus.Hidden;

                _context.SaveChanges();
                List<Comment> cs = _context.Comments.Where(x => x.ParentCommentId == commentId).ToList();
                if (cs != null)
                {
                    foreach (Comment comment in cs)
                    {
                        var cm = _context.Comments.FirstOrDefault(x => x.CommentId == comment.CommentId);
                        cm.CommentStatus = CommentStatus.Hidden;
                        _context.SaveChanges();
                    }
                }
                List<Report> cr = _context.Reports.Where(x => x.CommentId == commentId).ToList();
                if (cr != null) {
                    foreach (Report comment in cr)
                    {
                        var cm = _context.Reports.FirstOrDefault(x => x.CommentId == comment.CommentId);
                        _context.Remove(cm);
                        _context.SaveChanges();
                    }
                }
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpDelete("Report/{reportId}")]
        public IActionResult DeleteReport(int reportId)
        {
            try
            {
                Report report = _context.Reports.FirstOrDefault(x => x.ReportId == reportId);
                if (report == null)
                {
                    return NotFound();
                }
                else {
                    Comment c = _context.Comments.FirstOrDefault(x => x.CommentId == report.CommentId);
                    if (c == null)
                    {
                        return NotFound();
                    }
                    c.CommentStatus = CommentStatus.Posted;

                    _context.SaveChanges();
                }
               
               
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        private int CountReplies(int commentId)
        {
            return _context.Comments.Where(x => x.ParentCommentId == commentId && x.CommentStatus != CommentStatus.Hidden).ToList().Count;
        }
    }
}
