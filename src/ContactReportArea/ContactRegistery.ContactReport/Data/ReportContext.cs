using ContactRegistery.ContactReport.Data.Interfaces;
using ContactRegistery.ContactReport.Entities;
using ContactRegistery.ContactReport.Settings;
using MongoDB.Driver;

namespace ContactRegistery.ContactReport.Data;

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