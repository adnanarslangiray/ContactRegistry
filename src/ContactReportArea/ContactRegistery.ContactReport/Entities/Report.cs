using ContactRegistery.ContactReport.Entities.Common;

namespace ContactRegistery.ContactReport.Entities;

public class Report : BaseEntity
{
    public ReportStatus Status { get; set; }

    public enum ReportStatus
    {
        Preparing = 1,
        Completed = 2
    }
}