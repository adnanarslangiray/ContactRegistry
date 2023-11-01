namespace ContactRegistry.ContactReport.DTOs;

public class ContactReportCreateDto
{
    public string ReportId { get; set; }
    public IList<ContactReportDetailDto> ReportDetails { get; set; } = new List<ContactReportDetailDto>();
}

public class ContactReportDetailDto
{
    public int ContactCount { get; set; }
    public int PhoneNumberCount { get; set; }
    public string Location { get; set; }
}