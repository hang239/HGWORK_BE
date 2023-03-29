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
        Task<User> GetUserById(int id);
        Task<List<User>> GetAll();
        Task<int> Create(User user);
    }
}
