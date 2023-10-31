using ContactRegistry.ContactReport.DTOs;

namespace ContactRegistry.ContactReport.Helpers
{
    public interface IReportCreateHelper
    {
        Task ContactReportProcess(ContactReportCreateDto reportDetails);
    }
}
