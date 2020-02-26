using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Influence.Data;
using Influence.Domain.Entities;
using AutoMapper;
using Influence.Data.Models;

namespace Influence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikeRepository _repository;
        private readonly IMapper _mapper;

        public LikesController(ILikeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Likes
        [HttpGet]
        public async Task<ActionResult<List<LikeModel>>> GetLikes()
        {
            try
            {
                var likesEntity = await _repository.GetAllLikes();
                if(likesEntity == null)
                {
                    return NotFound();
                }
                return _mapper.Map<List<LikeModel>>(likesEntity);
            }
            catch(Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }

        //get by id and put might not be necessary
        // GET: api/Likes/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Like>> GetLike(int id)
        //{
        //    var like = await _context.Likes.FindAsync(id);

        //    if (like == null)
        //    {
        //        return NotFound();
        //    }

        //    return like;
        //}

        // PUT: api/Likes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutLike(int id, Like like)
        //{
        //    if (id != like.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(like).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LikeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Likes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<LikeModel>> PostLike(LikeModel like)
        {
            try
            {
                var likeEnitty = _mapper.Map<Like>(like);
                _repository.Add(likeEnitty);
                if(await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<LikeModel>(likeEnitty);
                }
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();
        }

        // DELETE: api/Likes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Like>> DeleteLike(int id)
        {
            try
            {
                var likeEntity = await _repository.GetLikeById(id);
                if (likeEntity == null)
                {
                    return NotFound();
                }

                _repository.Delete(likeEntity);
                if(await _repository.SaveChangesAsync())
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
