using ContactRegistery.ContactReport.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactRegistery.ContactReport.Controllers;

[Route("api")]
[ApiController]
public class ReportsController : ControllerBase
{

    private readonly IReportRepository _reportService;

    public ReportsController(IReportRepository reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("reports")]
    public async Task<IActionResult> GetReports()
    {
        var result = await _reportService.GetReportsAsync();
        return Ok(result);
    }

    [HttpGet("reports/{id}")]
    public async Task<IActionResult> GetReportById(string id)
    {
        var result = await _reportService.GetReportByIdAsync(id);
        return Ok(result);
    }

    [HttpPost("reports")]
    public async Task<IActionResult> CreateReport()
    {
        var result = await _reportService.CreateReportAsync();
        return Ok(result);
    }

    [HttpPut("reports/{id}")]
    public async Task<IActionResult> UpdateReportStatusById(string id)
    {
        var result = await _reportService.UpdateReportStatusAsync(id);
        return Ok(result);
    }


}