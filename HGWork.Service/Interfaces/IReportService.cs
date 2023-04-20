using HGWork.DTO.Report;
using HGWork.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGWork.Service.Interfaces
{
    public interface IReportService
    {
        Task<ResponseBase<ReportResultDto>> Report();
    }
}
