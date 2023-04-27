using HGWork.DTO;
using HGWork.Model;

namespace HGWork.Service.Interfaces
{
    public interface IProjectService
    {
        Task<ResponseBase<List<ProjectView>>> GetAll();
        Task<ResponseBase<Project>> GetById(int id);
        Task<ResponseBase<int>> Create(ProjectDto project);
        Task<ResponseBase<int>> Update(Project request);
        Task<ResponseBase<List<TaskView>>> GetTasks(int id);
    }
}
