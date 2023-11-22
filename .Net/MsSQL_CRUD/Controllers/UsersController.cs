using API_MSSQL.Models;
using API_MSSQL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API_MSSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public UsersController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _userRepository.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            _userRepository.Add(user);
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }


        [HttpPut("{id}")]
        public ActionResult<User> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _userRepository.Update(user);
            return Ok();
        }


        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(int id)
        {
            var user = _userRepository.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.Delete(user);
            return user;
        }
    }
}
