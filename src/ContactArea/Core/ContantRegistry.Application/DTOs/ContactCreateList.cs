namespace ContantRegistry.Application.DTOs;

public class ContactCreateList
{
    public string ReportId { get; set; }
    public IList<ReportDetailDto> ReportDetails { get; set; } = new List<ReportDetailDto>();
}

public class ReportDetailDto
{
    public int ContactCount { get; set; }
    public int PhoneNumberCount { get; set; }
    public string Location { get; set; }
}