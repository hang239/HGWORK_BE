using Microsoft.AspNetCore.Mvc;

namespace HGWork.Controllers
{
    [ApiController]
    [Route("project")]
    public class ProjectController : ControllerBase
    {
        [HttpGet]
        public int Index()
        {
            return 0;
        }
    }
}
