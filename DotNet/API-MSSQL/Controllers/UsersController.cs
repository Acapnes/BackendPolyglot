using API_MSSQL.Data;
using API_MSSQL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace API_MSSQL.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/Users
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

         //PUT: api/Users/5
        [HttpPut("{id}")]
        public ActionResult<User> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            //_context.Entry(user).State = Entity.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }
    }
}
