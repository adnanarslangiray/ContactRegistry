using ContactRegistry.ContactReport.DTOs;
using ContactRegistry.ContactReport.Entities;
using ContactRegistry.ContactReport.Repositories.Interfaces;

namespace ContactRegistry.ContactReport.Helpers;

public class ReportCreateHelper : IReportCreateHelper
{
    private readonly IReportRepository _reportRepository;

    public ReportCreateHelper(IReportRepository reportRepository)
    {
        _reportRepository=reportRepository;
    }

    public async Task ContactReportProcess(ContactReportCreateDto reportDetails)
    {
        var report = await _reportRepository.GetReportByIdAsync(reportDetails.ReportId);
        if (report is null)
            return;
        if (reportDetails.ReportDetails.Count == 0)
        {
            await _reportRepository.UpdateReportStatusAsync(reportDetails.ReportId, Report.ReportStatus.Completed);//failed verilebilir!
            return;
        }

        IList<ReportDetail> detailData = reportDetails.ReportDetails
            .Select(x => new ReportDetail() { ReportId = reportDetails.ReportId, ContactCount = x.ContactCount, Location = x.Location, PhoneNumberCount = x.PhoneNumberCount })
            .ToList();

        await _reportRepository.CreateReportDetailsAsync(detailData);

        await _reportRepository.UpdateReportStatusAsync(reportDetails.ReportId, Report.ReportStatus.Completed);
    }
}