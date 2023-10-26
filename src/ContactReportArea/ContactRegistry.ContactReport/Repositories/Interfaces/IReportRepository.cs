using ContactRegistry.Common.Utilities;
using ContactRegistry.ContactReport.Entities;

namespace ContactRegistry.ContactReport.Repositories.Interfaces;

public interface IReportRepository
{
    Task<BaseResponse<IList<Report>>> GetReportsAsync();
    Task<Report> GetReportByIdAsync(string id);
    Task<Report> CreateReportAsync();
    Task<Report> UpdateReportStatusAsync(string id, Report.ReportStatus status);
    Task<IList<ReportDetail>> GetReportDetailsByReportIdAsync(string reportId);
    Task CreateReportDetailsAsync(IList<ReportDetail> reportDetails);
    Task<IList<ReportDetail>> GetReportDetailsAsync();
}
