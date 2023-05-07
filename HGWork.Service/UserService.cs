﻿using HGWork.DTO;
using HGWork.Model;
using HGWork.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace HGWork.Service
{
    public class UserService : IUserService
    {
        private readonly HGWorkDbContext _context;
        private readonly ILogger _logger;
        private readonly IEmailService _emailService;
        public UserService(HGWorkDbContext context, ILogger<UserService> logger, IEmailService emailService)
        {
            _context = context;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<ResponseBase<User>> GetUserById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            return new ResponseBase<User>()
            {
                Data = user,
                StatusCode = 200,
                Message = "Success"
            };
        }

        public async Task<ResponseBase<List<User>>> GetAll()
        {
            var res = await _context.Users.OrderByDescending(x => x.Id).ToListAsync();
            return new ResponseBase<List<User>>()
            {
                Data = res,
                StatusCode = 200,
                Message = "Success"
            };
        }

        public async Task<ResponseBase<List<User>>> Filter(string filter)
        {
            var res = await _context.Users.OrderByDescending(x => x.Id).ToListAsync();

            if (!string.IsNullOrEmpty(filter))
            {
                res = res.Where(x => x.Name.Contains(filter)).ToList();
            }
            return new ResponseBase<List<User>>()
            {
                Data = res,
                StatusCode = 200,
                Message = "Success"
            };
        }

        public async Task<ResponseBase<int>> Create(User user)
        {
            if (user != null)
            {
                user.CreatedDate = DateTime.Now;
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return new ResponseBase<int>()
                {
                    Data = 1,
                    StatusCode = 200,
                    Message = "Success"
                };
            }
            return new ResponseBase<int>()
            {
                Data = 1,
                StatusCode = 400,
                Message = "Bad request"
            };
        }

        public async Task<ResponseBase<int>> Update(User request)
        {
            try
            {
                if (request != null)
                {
                    _context.Users.Update(request);
                    await _context.SaveChangesAsync();
                    return new ResponseBase<int>
                    {
                        StatusCode = 200,
                        Data = 1,
                        Message = "Thành công"
                    };
                }
                return new ResponseBase<int>
                {
                    StatusCode = 400,
                    Data = 0,
                    Message = "Dữ liệu không đúng định dạng"
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<int>
                {
                    StatusCode = 400,
                    Data = 0,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseBase<int>> Delete(int userId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return new ResponseBase<int>
                {
                    StatusCode = 200,
                    Data = 1,
                    Message = "Thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<int>
                {
                    StatusCode = 400,
                    Data = 0,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseBase<User>> Login(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName.Equals(userName) && x.Password.Equals(password));
            if (user != null && user.Id > 0)
            {
                return new ResponseBase<User>()
                {
                    Data = user,
                    StatusCode = 200,
                    Message = "Success"
                };
            }

            return new ResponseBase<User>()
            {
                Data = new User(),
                StatusCode = 500,
                Message = "Sai tài khoản hoặc mật khẩu"
            };
        }

        public async Task<ResponseBase<List<TaskView>>> GetTaskByUser(int userId, int status)
        {
            var tasks = _context.Tasks.Where(x => x.UserId == userId).ToList();

            if (status > 0)
            {
                tasks = tasks.Where(x => x.Status == status).ToList();
            }

            var res = tasks.Select(x => new TaskView()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Description = x.Description,
                StartDate = x.StartDate.ToString("MM/dd/yyyy"),
                EndDate = x.EndDate.ToString("MM/dd/yyyy"),
                Status = x.Status
            }).ToList();

            return new ResponseBase<List<TaskView>> { StatusCode = 200, Data = res, Message = "Filter success" };
        }

        public async Task<ResponseBase<int>> ForgotPassword(string username , string email)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName.Equals(username) && x.Email.Equals(email));
            if (user != null && user.Id > 0)
            {
                // send mail
                this.SendMailForgot(user.UserName, user.Password, email);
                return new ResponseBase<int> { StatusCode = 200, Data = 0, Message = "Forgot Password success" };
            }
            return new ResponseBase<int> { StatusCode = 400, Data = 0, Message = "User not exist" };
        }

        public void SendMailForgot(string username, string password, string mailTo)
        {
            string contentEmail = $"" +
            "<p>Email xác nhận tài khoản:</p>" +
                "<ul>" +
                    $"<li> Tài khoản: <b>{username}</b></li>" +
                    $"<li> Mật khẩu: <b>{password}</b></li>" +
                "</ul>" +
            "<p>Chúng tôi gửi thông báo này tới bạn để xác nhận các thông tin.</p>" +
            "<p>Cảm ơn bạn đã tin dùng hệ thống của chúng tôi!</p>";

            var email = new Email()
            {
                From = "",
                EmailContent = contentEmail,
                Subject = "Thông báo xác nhận tài khoản",
                To = mailTo
            };

            _ = _emailService.SendMail(email);
        }
    }
}