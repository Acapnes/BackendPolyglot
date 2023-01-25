using API_MSSQL.Models;
using API_MSSQL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API_MSSQL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersIRepoController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public UsersIRepoController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }
    }
}
