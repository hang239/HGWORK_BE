using HGWork.DTO;
using HGWork.DTO.Report;
using HGWork.Model;
using HGWork.Service;
using HGWork.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HGWork.Controllers
{
    [ApiController]
    [Route("dashboard")]
    public class DashboardController : Controller
    {
        private readonly IReportService _reportService;

        public DashboardController(IReportService reportService)
        {
            _reportService = reportService;
        }


        [HttpGet("report")]
        public async Task<ResponseBase<ReportResultDto>> Get()
        {
            var res = await _reportService.Report();
            return res;
        }
    }
}
