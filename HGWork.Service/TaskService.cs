using AutoMapper;
using HGWork.DTO;
using HGWork.Model;
using HGWork.Service.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using HGWork.Helper.Email;
using HGWork.Helper.Enums;

namespace HGWork.Service
{
    public class TaskService : ITaskService
    {
        private readonly HGWorkDbContext _context;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public TaskService(HGWorkDbContext context, ILogger<UserService> logger, IMapper mapper, IEmailService emailService)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<ResponseBase<int>> Create(Model.Task request)
        {
            try
            {
                if (request != null)
                {
                    await _context.Tasks.AddAsync(request);
                    await _context.SaveChangesAsync();

                    var user = _context.Users.FirstOrDefault(x => x.Id == request.UserId);
                    // send mail
                    string status = TaskStatusEnum.Backlog.ToString();
                    if (request.Status == 1)
                    {
                        status = TaskStatusEnum.Doing.ToString();
                    }
                    if (request.Status == 2)
                    {
                        status = TaskStatusEnum.Done.ToString();
                    }
                    if (request.Status == 3)
                    {
                        status = TaskStatusEnum.Pending.ToString();
                    }
                    if (request.Status == 4)
                    {
                        status = TaskStatusEnum.Canceled.ToString();
                    }
                    this.SendMail(request.Name, status, "http://localhost:8080/#/updatetask/" + request.Id.ToString(), request.StartDate.ToString("MM/dd/yyyy"), request.EndDate.ToString("MM/dd/yyyy"), user.Email);

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

        public async Task<ResponseBase<int>> Update(Model.Task request)
        {
            try
            {
                if (request != null)
                {
                    var user = _context.Users.FirstOrDefault(x => x.Id == request.UserId);
                    // send mail
                    string status = TaskStatusEnum.Backlog.ToString();
                    if (request.Status == 1)
                    {
                        status = TaskStatusEnum.Doing.ToString();
                    }
                    if (request.Status == 2)
                    {
                        status = TaskStatusEnum.Done.ToString();
                    }
                    if (request.Status == 3)
                    {
                        status = TaskStatusEnum.Pending.ToString();
                    }
                    if (request.Status == 4)
                    {
                        status = TaskStatusEnum.Canceled.ToString();
                    }
                    this.SendMail(request.Name, status, "http://localhost:8080/#/updatetask/" + request.Id.ToString(), request.StartDate.ToString("MM/dd/yyyy"), request.EndDate.ToString("MM/dd/yyyy"), user.Email);

                    _context.Tasks.Update(request);
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

        public async Task<ResponseBase<Model.Task>> GetById(int id)
        {
            try
            {
                var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);


                return new ResponseBase<Model.Task>
                {
                    StatusCode = 200,
                    Data = task,
                    Message = "Thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<Model.Task>
                {
                    StatusCode = 400,
                    Data = null,
                    Message = ex.Message
                };
            }

        }

        public async Task<ResponseBase<List<TaskView>>> GetAll()
        {
            try
            {
                var tasks = await _context.Tasks.ToListAsync();
                var res = tasks.Select(x => new TaskView()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Description = x.Description,
                    StartDate = x.StartDate.ToString("MM/dd/yyyy"),
                    EndDate = x.EndDate.ToString("MM/dd/yyyy")
                }).ToList();

                return new ResponseBase<List<TaskView>>
                {
                    StatusCode = 200,
                    Data = res,
                    Message = "Thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<TaskView>>
                {
                    StatusCode = 400,
                    Data = null,
                    Message = ex.Message
                };
            }

        }

        public async Task<ResponseBase<List<TaskView>>> FilterTasks(FilterTaskDto filter)
        {
            var tasks = new List<Model.Task>();
            if (filter.ProjectId > 0)
            {
                tasks = tasks.Where(x => x.ProjectId == filter.ProjectId).ToList();
            }
            if (filter.UserId > 0)
            {
                tasks = tasks.Where(x => x.UserId == filter.UserId).ToList();
            }
            if (filter.Status > 0)
            {
                tasks = tasks.Where(x => x.Status == filter.Status).ToList();
            }
            if (filter.StartDate != null && filter.EndDate != null)
            {
                tasks = tasks.Where(x => x.StartDate <= filter.StartDate && x.EndDate >= filter.EndDate).ToList();
            }

            var res = tasks.Select(x => new TaskView()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Description = x.Description,
                    StartDate = x.StartDate.ToString("MM/dd/yyyy"),
                    EndDate = x.EndDate.ToString("MM/dd/yyyy")
                }).ToList();

            return new ResponseBase<List<TaskView>> { StatusCode = 200, Data = res, Message = "Filter success"};
        }

        public void SendMail(string name, string status, string startDate, string endDate, string link, string mailTo)
        {
            string contentEmail = string.Format(@"
            <p>Thông báo cập nhật công việc từ HGWork</p>
            <p>Hệ thống thông báo HGWork<p>
            <p>Thông tin task:</p>
                <ul>
                    <li> Task: <b>{0}</b></li>
                    <li> Link: <b>{1}</b></li>
                    <li> Ngày bắt đầu: <b>{2}</b></li>
                    <li> Ngày kết thúc: <b>{3}</b></li>
                    <li> Trạng thái: <b>{4}</b></li>
                    <li> Ngày cập nhật: <b>{5}</b></li>
                </ul>
            <p>Chúng tôi gửi thông báo này tới bạn để xác nhận các thông tin.</p>
            <p>Cảm ơn bạn đã tin dùng hệ thống của chúng tôi!</p>
            ", name, link, startDate, endDate, status, DateTime.Now);

            var email = new Email()
            {
                From = "",
                EmailContent = contentEmail,
                Subject = "Thông báo cập nhật công việc",
                To = mailTo
            };
            
            _ = _emailService.SendMail(email);
        }

        public async Task<List<Model.Task>> GetTaskEndDate()
        {
            var tasks = new List<Model.Task>();
            var now = DateTime.Now.AddHours(-1);

            tasks = tasks.Where(x => x.EndDate.ToString("MM/dd/yyyy").Equals(now.ToString("MM/dd/yyyy"))).ToList();

            return tasks;
        }
    }
}
