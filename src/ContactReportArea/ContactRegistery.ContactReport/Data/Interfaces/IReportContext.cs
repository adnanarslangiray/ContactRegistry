using ContactRegistery.ContactReport.Entities;
using MongoDB.Driver;

namespace ContactRegistery.ContactReport.Data.Interfaces;

public interface IReportContext
{
    IMongoCollection<Report> Reports { get; }
    IMongoCollection<ReportDetail> ReportDetails { get; }
}
