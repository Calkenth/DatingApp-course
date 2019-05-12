using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //validate request

            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();

            if(await _repo.UserExist(userForRegisterDto.UserName))
                return BadRequest("User already exist");

            var userToCreate = new User{UserName = userForRegisterDto.UserName};
            var createdUser = await _repo.Register(userToCreate,userForRegisterDto.Password);

            return StatusCode(201);
        }
    }
}