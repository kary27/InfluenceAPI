using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Influence.Data;
using Influence.Domain.Entities;
using AutoMapper;
using Influence.Data.Models;

namespace Influence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;

        public PostsController(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<List<PostModel>>> GetPosts()
        {
            try
            {
                var postsEntity = await _repository.GetAllPostsAsync();
                return _mapper.Map<List<PostModel>>(postsEntity);
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            try
            {
                var postEntity = await _repository.GetPostAsync(id);

                if (postEntity == null)
                {
                    return NotFound();
                }

                return _mapper.Map<Post>(postEntity);
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();
        }

        [HttpGet("postscomm")]
        public async Task<ActionResult<List<PostModel>>> GetPostsWithComments()
        {
            try
            {
                var postsEntity = await _repository.GetAllPostsWithComments();

                if (postsEntity == null)
                {
                    return NotFound();
                }

                return _mapper.Map<List<PostModel>>(postsEntity);
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<PostModel>> PutPost(int id, PostModel postModel)
        {
            try
            {
                var postEntity = await _repository.GetPostAsync(id);

                if (postEntity == null)
                {
                    return NotFound();
                }

                _mapper.Map(postModel, postEntity);

                _repository.Update(postEntity);

                if (await _repository.SaveChangesAsync())
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError);
            }


            return BadRequest();
            
        }

        // POST: api/Posts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PostModel>> PostPost(PostModel postModel)
        {
            try
            {
                var postEntity = _mapper.Map<Post>(postModel);
                if (postEntity == null)
                {
                    return BadRequest();
                }
                _repository.Add(postEntity);

                if (await _repository.SaveChangesAsync())
                {
                    return CreatedAtAction("GetPost", new { id = postEntity.Id }, postEntity);
                }
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> DeletePost(int id)
        {
            try
            {
                var postEntity = await _repository.GetPostAsync(id);
                if (postEntity == null)
                {
                    return NotFound();
                }

                _repository.Delete(postEntity);
                if (await _repository.SaveChangesAsync())
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();
        }

    }
}
