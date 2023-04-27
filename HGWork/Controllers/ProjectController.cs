using HGWork.DTO;
using HGWork.Model;
using HGWork.Service.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HGWork.Controllers
{
    [ApiController]
    [Route("project")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("getall")]
        public async Task<ResponseBase<List<ProjectView>>> Get()
        {
            var res = await _projectService.GetAll();
            return res;
        }

        [HttpGet("detail/{id}")]
        public async Task<ResponseBase<Project>> Detail(int id)
        {
            var res = await _projectService.GetById(id);
            return res;
        }

        [HttpGet("gettasks/{id}")]
        public async Task<ResponseBase<List<TaskView>>> GetTasks(int id)
        {
            var res = await _projectService.GetTasks(id);
            return res;
        }

        [HttpPost("create")]
        public async Task<ResponseBase<int>> Create([FromBody] ProjectDto project)
        {
            var res = await _projectService.Create(project);
            return res;
        }

        [HttpPost("update")]
        public async Task<ResponseBase<int>> Update([FromBody] Project project)
        {
            var res = await _projectService.Update(project);
            return res;
        }
    }
}
