using HGWork.DTO;
using HGWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGWork.Service.Interfaces
{
    public interface IUserService
    {
        Task<ResponseBase<User>> GetUserById(int id);
        Task<ResponseBase<List<User>>> GetAll();
        Task<ResponseBase<int>> Create(User user);
        Task<ResponseBase<int>> Update(User request);
        Task<ResponseBase<User>> Login(string userName, string password);
        Task<ResponseBase<List<TaskView>>> GetTaskByUser(int userId, int status);
        Task<ResponseBase<int>> Delete(int userId);
        Task<ResponseBase<List<User>>> Filter(string filter);
    }
}
