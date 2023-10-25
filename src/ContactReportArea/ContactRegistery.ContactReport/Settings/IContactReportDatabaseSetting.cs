namespace ContactRegistry.ContactReport.Settings;

public interface IContactReportDatabaseSetting
{
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
    string CollectionName { get; set; }
    string ContactReportCollectionName { get; set; }
}