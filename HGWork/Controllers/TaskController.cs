using HGWork.DTO;
using HGWork.Model;
using HGWork.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HGWork.Controllers
{
    [ApiController]
    [Route("task")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("getall")]
        public async Task<ResponseBase<List<TaskView>>> Get()
        {
            var res = await _taskService.GetAll();
            return res;
        }

        [HttpGet("filter/{name}")]
        public async Task<ResponseBase<List<TaskView>>> Filter(string name)
        {
            var res = await _taskService.Filter(name);
            return res;
        }

        [HttpGet("detail/{id}")]
        public async Task<ResponseBase<Model.Task>> Detail(int id)
        {
            var res = await _taskService.GetById(id);
            return res;
        }

        [HttpPost("create")]
        public async Task<ResponseBase<int>> Create([FromBody]Model.Task request)
        {
            var res = await _taskService.Create(request);
            return res;
        }

        [HttpPost("update")]
        public async Task<ResponseBase<int>> Update([FromBody] Model.Task request)
        {
            var res = await _taskService.Update(request);
            return res;
        }

        //[HttpGet("filter/{name}")]
        //public async Task<ResponseBase<List<ProjectView>>> Filter(string name)
        //{
        //    var res = await _taskService.Filter(name);
        //    return res;
        //}
    }
}
