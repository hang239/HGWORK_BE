using AutoMapper;
using HGWork.DTO;
using HGWork.Model;
using HGWork.Service.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

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
    }
}
