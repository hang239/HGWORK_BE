using AutoMapper;
using HGWork.DTO;
using HGWork.DTO.Report;
using HGWork.Helper.Enums;
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
    public class ReportService : IReportService
    {
        private readonly HGWorkDbContext _context;

        public ReportService(HGWorkDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseBase<ReportResultDto>> Report()
        {
            var projects = await _context.Projects.ToListAsync();
            var tasks = await _context.Tasks.ToListAsync();
            var report = new ReportResultDto()
            {
                TotalProjects = projects.Count,
                TotalTasks = tasks.Count,
                TotalTasksDoing = tasks.Where(x => x.Status == (int)TaskStatusEnum.Doing).Count(),
                TotalTasksPending = tasks.Where(x => x.Status == (int)TaskStatusEnum.Pending).Count(),
                TotalTasksCanceled = tasks.Where(x => x.Status == (int)TaskStatusEnum.Canceled).Count(),
                TotalTasksDone = tasks.Where(x => x.Status == (int)TaskStatusEnum.Done).Count(),
                DataTotalProjects = this.DataProject(),
                DataTotalTasks = this.DataTask()
            };
            
            return new ResponseBase<ReportResultDto> { Data = report, StatusCode = 200, Message = "Success" };
        } 

        private int[] DataProject()
        {
            var data = new List<int>();
            var project = _context.Projects.ToList();

            for (int i = 1; i <= 12; i++)
            {
                var count = project.Count(x => x.StartDate.Month == i);
                data.Add(count);
            }
            return data.ToArray();
        }

        private int[] DataTask()
        {
            var data = new List<int>();
            var tasks = _context.Tasks.ToList();

            for (int i = 1; i <= 12; i++)
            {
                var count = tasks.Count(x => x.StartDate.Month == i);
                data.Add(count);
            }
            return data.ToArray();
        }
    }
}
