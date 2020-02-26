using AutoMapper;
using Influence.Data;
using Influence.Data.Models;
using Influence.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Influence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;

        public CommentsController(ICommentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<List<CommentModel>>> GetComments()
        {

            try
            {
                var commentsEntity = await _repository.GetAllCommentssAsync();
                return _mapper.Map<List<CommentModel>>(commentsEntity);
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();
            
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentModel>> GetComment(int id)
        {
            try
            {
                var commentEnity = await _repository.GetComment(id);
                if(commentEnity == null)
                {
                    return NotFound();
                }

                return _mapper.Map<CommentModel>(commentEnity);
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            return BadRequest();
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<CommentModel>> PutComment(int id, CommentModel commentModel)
        {

            try
            {
                var commentEntity = await _repository.GetComment(id);
                if (commentEntity == null)
                {
                    return NotFound();
                }

                _mapper.Map(commentModel, commentEntity);
                _repository.Update(commentEntity);

                if(await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<CommentModel>(commentEntity);
                }
            }
            catch
            {
                StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();

           
        }

        // POST: api/Comments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CommentModel>> PostComment(CommentModel comment)
        {
            try
            {
                var commentEntity = _mapper.Map<Comment>(comment);
                _repository.Add(commentEntity);
                if(await _repository.SaveChangesAsync())
                {
                    return CreatedAtAction("GetComment", new { id = commentEntity.Id }, commentEntity);
                }

            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();

        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CommentModel>> DeleteComment(int id)
        {

            try
            {
                var commentEntity = await _repository.GetComment(id);
                if(commentEntity == null)
                {
                    return NotFound();
                }

                _repository.Delete(commentEntity);
                if(await _repository.SaveChangesAsync())
                {
                    return NotFound();
                }

            }
            catch
            {
                StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();
        }

    }
}
