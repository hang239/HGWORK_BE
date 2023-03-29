using HGWork.Model;
using HGWork.Repository.Interfaces;

namespace HGWork.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly HGWorkDbContext _context;
        public UserRepository(HGWorkDbContext _context)
        {

        }
    }
}