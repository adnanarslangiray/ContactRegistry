using ContactRegistery.ContactReport.Data.Interfaces;
using ContactRegistery.ContactReport.Entities;
using ContactRegistery.ContactReport.Repositories.Interfaces;
using ContactRegistry.Common.Utilities;
using MongoDB.Driver;

namespace ContactRegistery.ContactReport.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly IReportContext _context;

    public ReportRepository(IReportContext context)
    {
        _context=context;
    }

    public async Task<Report> CreateReportAsync()
    {
        var report = new Report
        {
            Status = Report.ReportStatus.Preparing
        };
        await _context.Reports.InsertOneAsync(report);
        return report;
    }

    public async Task<Report> UpdateReportStatusAsync(string id)
    {
        var filter = Builders<Report>.Filter.Eq(r => r.Id, id);
        var update = Builders<Report>.Update
            .Set(r => r.Status, Report.ReportStatus.Completed)
            .Set(r => r.UpdatedDate, DateTime.UtcNow);
        var options = new FindOneAndUpdateOptions<Report>
        {
            ReturnDocument = ReturnDocument.After
        };
        return await _context.Reports.FindOneAndUpdateAsync(filter, update, options);
    }

    public async Task<Report> GetReportByIdAsync(string id)
    {
        return await _context.Reports.Find(r => r.Id == id).FirstOrDefaultAsync();
    }

    public async Task<BaseResponse<IList<Report>>> GetReportsAsync()
    {
        var result = await _context.Reports.Find(r => true).ToListAsync();

        return new BaseResponse<IList<Report>>
        {
            Data = result,
            Success = true
        };
    }

    public async Task<IList<ReportDetail>> GetReportDetailsByReportIdAsync(string reportId)
    {
        return await _context.ReportDetails.Find(r => r.ReportId == reportId).ToListAsync();
    }

    public async Task CreateReportDetailsAsync(IList<ReportDetail> reportDetails)
    {
        await _context.ReportDetails.InsertManyAsync(reportDetails);
    }
}