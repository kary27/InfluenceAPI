using AutoMapper;
using Influence.Data;
using Influence.Domain.Entities;
using Influence.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Influence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<List<UserModel[]>>> GetUsers()
        {
            try
            {
                var results = await _repository.GetAllUsersAsync();

                UserModel[] models = _mapper.Map<UserModel[]>(results);

                return Ok(models);
            }
            catch (Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUser(int id)
        {
            try
            {
                var user = await _repository.GetUserAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                return _mapper.Map<UserModel>(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> PutUser(int id, UserModel user)
        {
            try
            {

                if (id != user.Id)
                {
                    return BadRequest();
                }

                var currentUser = await _repository.GetUserAsync(id);
                if (currentUser == null)
                {
                    return NotFound();
                }
                _mapper.Map(user, currentUser);
                _repository.UpdateUser(currentUser);
                

                if(await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<UserModel>(currentUser);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UserModel>> PostUser(UserModel user)
        {
            try
            {
                var userEntity = _mapper.Map<User>(user);
                _repository.Add(userEntity);
                if(await _repository.SaveChangesAsync())
                {
                    return  CreatedAtAction("GetUser", new { id = user.Id }, user);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> DoLogin(UserModel user)
        {
            try
            {
                var existingUser = await _repository.GetUserByName(user.UserName);
                if (existingUser == null)
                {
                    return NotFound();
                }

                return existingUser.Password == user.Password ? _mapper.Map<UserModel>(existingUser): null;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var userEnitty = await _repository.GetUserAsync(id);
                if(userEnitty == null)
                {
                    return NotFound($"The user with id {id} is not in the database");
                }

                _repository.Delete(userEnitty);
                if (await _repository.SaveChangesAsync())
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();

        }
    }
}
