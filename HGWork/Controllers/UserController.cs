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

        [HttpGet("filter/{name}")]
        public async Task<ResponseBase<List<User>>> Filter(string name)
        {
            return await _userService.Filter(name);
        }

        [HttpGet("getuser/{id}")]
        public async Task<ResponseBase<User>> GetUser(int id)
        {
            return await _userService.GetUserById(id);
        }

        [HttpGet("gettaskbyuser")]
        public async Task<ResponseBase<List<TaskView>>> GetTaskByUser([FromQuery] int userId, [FromQuery] int status)
        {
            return await _userService.GetTaskByUser(userId, status);
        }


        [HttpPost("create")]
        public async Task<ResponseBase<int>> Create([FromBody] User user)
        {
            return await _userService.Create(user);
        }

        [HttpPost("update")]
        public async Task<ResponseBase<int>> Update([FromBody] User user)
        {
            return await _userService.Update(user);
        }

        [HttpGet("delete")]
        public async Task<ResponseBase<int>> Delete(int id)
        {
            return await _userService.Delete(id);
        }
    }
}
