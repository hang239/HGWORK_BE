using HGWork.DTO;
using HGWork.Model;
using HGWork.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HGWork.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("getall")]
        public async Task<ResponseBase<List<User>>> GetAll()
        {
            return await _userService.GetAll();
        }

        [HttpGet("getuser/{id}")]
        public async Task<ResponseBase<User>> GetUser(int id)
        {
            return await _userService.GetUserById(id);
        }


        [HttpPost("create")]
        public async Task<ResponseBase<int>> Create([FromBody] User user)
        {
            return await _userService.Create(user);
        }
    }
}
