﻿using ContactRegistry.ContactReport.Entities.Common;

namespace ContactRegistry.ContactReport.Entities;

public class Report : BaseEntity
{
    public ReportStatus Status { get; set; }
    public string StatusValue => Status.ToString();
    public enum ReportStatus
    {
        Preparing = 1,
        Completed = 2,
        Failed = 3
    }
}