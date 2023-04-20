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
        Task<ResponseBase<List<Model.Task>>> GetAll();
        Task<ResponseBase<List<Model.Task>>> FilterTasks(FilterTaskDto filter);
    }
}
