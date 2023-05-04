using HGWork.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGWork.Service.Interfaces
{
    public interface ITaskService
    {
        Task<ResponseBase<int>> Create(Model.Task request);
        Task<ResponseBase<int>> Update(Model.Task request);
        Task<ResponseBase<Model.Task>> GetById(int id);
        Task<ResponseBase<List<TaskView>>> GetAll();
        Task<ResponseBase<List<TaskView>>> FilterTasks(FilterTaskDto filter);
        Task<ResponseBase<List<TaskView>>> Filter(string filter);
        Task<List<Model.Task>> GetTaskEndDate();
    }
}
