using ContactRegistry.ContactReport.Data.Interfaces;
using ContactRegistry.ContactReport.Entities;
using ContactRegistry.ContactReport.Settings;
using MongoDB.Driver;

namespace ContactRegistry.ContactReport.Data;

public class ReportContext : IReportContext
{
    public IMongoCollection<Report> Reports { get; }

    public IMongoCollection<ReportDetail> ReportDetails { get; }

    public ReportContext(IContactReportDatabaseSetting settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);

        Reports = database.GetCollection<Report>(settings.CollectionName);
        ReportDetails = database.GetCollection<ReportDetail>(settings.ContactReportCollectionName);
    }
}