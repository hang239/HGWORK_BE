using HGWork.Model;
using HGWork.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HGWork.Service
{
    public class UserService : IUserService
    {
        private readonly HGWorkDbContext _context;
        private readonly ILogger _logger;
        public UserService(HGWorkDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<int> Create(User user)
        {
            await _context.AddAsync(user);
            return _context.SaveChanges();
        }
    }
}