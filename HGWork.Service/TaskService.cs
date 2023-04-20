using AutoMapper;
using HGWork.DTO;
using HGWork.Model;
using HGWork.Service.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using HGWork.Helper.Email;

namespace HGWork.Service
{
    public class TaskService : ITaskService
    {
        private readonly HGWorkDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public TaskService(HGWorkDbContext context, ILogger<UserService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
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

                    this.SendMail(request.Name, "http:\\localhost:8080", request.Description, user.Email);

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
                    var project = _context.Projects.FirstOrDefault(x => x.Id == request.Id);
                    if (project == null)
                    {
                        return new ResponseBase<int>
                        {
                            StatusCode = 400,
                            Data = 0,
                            Message = "Không tìm thấy dữ liệu"
                        };
                    }
                    var user = _context.Users.FirstOrDefault(x => x.Id == request.UserId);
                    // send mail
                    this.SendMail(request.Name, "http:\\localhost:8080", request.Description, user.Email);

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

        public async Task<ResponseBase<List<Model.Task>>> GetAll()
        {
            try
            {
                var tasks = await _context.Tasks.ToListAsync();


                return new ResponseBase<List<Model.Task>>
                {
                    StatusCode = 200,
                    Data = tasks,
                    Message = "Thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Model.Task>>
                {
                    StatusCode = 400,
                    Data = null,
                    Message = ex.Message
                };
            }

        }

        public async Task<ResponseBase<List<Model.Task>>> FilterTasks(FilterTaskDto filter)
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

            return new ResponseBase<List<Model.Task>> { StatusCode = 200, Data = tasks , Message = "Filter success"};
        }

        public void SendMail(string name, string link, string des, string mailTo)
        {
            string contentEmail = string.Format(@"
            <p>Thông báo cập nhật công việc từ HGWork</p>
            <p>Hệ thống thông báo HGWork<p>
            <p>Thông tin task:</p>
                <ul>
                    <li> Task: <b>{0}</b></li>
                    <li> Link: <b>{1}</b></li>
                    <li> Ngày: <b>{2}</b></li>
                </ul>
            <p>Chúng tôi gửi thông báo này tới bạn để xác nhận các thông tin.</p>
            <p>Cảm ơn bạn đã tin dùng hệ thống của chúng tôi!</p>
            <p><i>Trung thâm hỗ trợ 24/7 HGWork</i></p>
            ", name, link, DateTime.Now);
            new System.Threading.Tasks.Task(() => { SMTPHelper.SendMail(mailTo, contentEmail, "Thông báo cập nhật công việc"); }).Start();
        }
    }
}
