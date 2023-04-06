using HGWork.Model;
using HGWork.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HGWork.Service
{
    public class AccessService : IAccessService
    {
        private readonly HGWorkDbContext _context;
        private readonly ILogger _logger;
        public AccessService(HGWorkDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool Login(User user)
        {
            var userLogin = _context.Users.FirstOrDefault(x => x.UserName.Equals(user.UserName) && x.Password.Equals(user.Password));
            if (userLogin != null)
            {
                //HttpContext.Session.SetInt32("UserID", userLogin.Id);
                //HttpContext.Session.SetString("UserName", userLogin.DisplayName.ToString());
                return true;
            }
            return false;
        }
    }
}
