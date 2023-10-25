namespace ContactRegistry.ContactReport.Settings;

public class ContactReportDatabaseSetting : IContactReportDatabaseSetting
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
    public string ContactReportCollectionName { get; set; }
}