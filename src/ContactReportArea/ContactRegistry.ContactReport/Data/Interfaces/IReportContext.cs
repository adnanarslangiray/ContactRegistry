using ContactRegistry.ContactReport.Entities;
using MongoDB.Driver;

namespace ContactRegistry.ContactReport.Data.Interfaces;

public interface IReportContext
{
    IMongoCollection<Report> Reports { get; }
    IMongoCollection<ReportDetail> ReportDetails { get; }

}
