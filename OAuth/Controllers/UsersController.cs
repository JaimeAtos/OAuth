using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAuth.Models;
using OAuth.Repository;
using System.Collections.Generic;

namespace OAuth.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IJWTManagerRepository _jWTManagerRepository;
        public UsersController(IJWTManagerRepository jWTManagerRepository)
        {
            _jWTManagerRepository = jWTManagerRepository;
        }

        [HttpGet]
        public List<string> Get()
        {
            var users = new List<string>
            {
                "Julio",
                "Daniel",
                "Jaime"
            };
            return users;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(Users usersData)
        {
            var token = _jWTManagerRepository.Authenticate(usersData);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
