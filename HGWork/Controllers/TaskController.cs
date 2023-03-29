using Microsoft.AspNetCore.Mvc;

namespace HGWork.Controllers
{
    [ApiController]
    [Route("task")]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public int Index()
        {
            return 0;
        }
    }
}
