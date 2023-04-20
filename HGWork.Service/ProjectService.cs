using AutoMapper;
using HGWork.DTO;
using HGWork.Model;
using HGWork.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace HGWork.Service
{
    public class ProjectService : IProjectService
    {
        private readonly HGWorkDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ProjectService(HGWorkDbContext context, ILogger<UserService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResponseBase<int>> Create(ProjectDto project)
        {
            try
            {
                if (project != null)
                {
                    var request = _mapper.Map<ProjectDto, Project>(project);

                    await _context.Projects.AddAsync(request);
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

        public async Task<ResponseBase<int>> Update(Project request)
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

                    _context.Projects.Update(request);
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

        public async Task<ResponseBase<Project>> GetById(int id)
        {
            try
            {
                var projects = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);


                return new ResponseBase<Project>
                {
                    StatusCode = 200,
                    Data = projects,
                    Message = "Thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<Project>
                {
                    StatusCode = 400,
                    Data = null,
                    Message = ex.Message
                };
            }

        }

        public async Task<ResponseBase<List<Project>>> GetAll()
        {
            try
            {
                var projects = await _context.Projects.ToListAsync();


                return new ResponseBase<List<Project>>
                {
                    StatusCode = 200,
                    Data = projects,
                    Message = "Thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Project>>
                {
                    StatusCode = 400,
                    Data = null,
                    Message = ex.Message
                };
            }
           
        }

        public async Task<ResponseBase<List<Model.Task>>> GetTasks(int id)
        {
            var tasks = new List<Model.Task>();
            if (id > 0)
            {
                tasks = await _context.Tasks.Where(x => x.ProjectId == id).ToListAsync();
            }
            else
            {
                tasks = await _context.Tasks.ToListAsync();
            }

            return new ResponseBase<List<Model.Task>>()
            {
                StatusCode = 200,
                Data = tasks,
                Message =  "Thành công"
            };
        }
        
    }
}
