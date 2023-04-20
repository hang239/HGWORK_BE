using HGWork.DTO;
using HGWork.Model;
using HGWork.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HGWork.Controllers
{
    [ApiController]
    [Route("access")]
    public class AccessController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccessController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("login")]
        public async Task<ResponseBase<bool>> Login([FromBody] LoginDto request)
        {
            return await _userService.Login(request.Username, request.Password);
        }
       
    }
}
