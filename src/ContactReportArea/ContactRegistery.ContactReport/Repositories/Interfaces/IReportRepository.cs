using ContactRegistery.ContactReport.Entities;
using ContactRegistry.Common.Utilities;

namespace ContactRegistery.ContactReport.Repositories.Interfaces;

public interface IReportRepository
{
    Task<BaseResponse<IList<Report>>> GetReportsAsync();
    Task<Report> GetReportByIdAsync(string id);
    Task<Report> CreateReportAsync();
    Task<Report> UpdateReportStatusAsync(string id);
    Task<IList<ReportDetail>> GetReportDetailsByReportIdAsync(string reportId);
    Task CreateReportDetailsAsync(IList<ReportDetail> reportDetails);
}
