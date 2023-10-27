using ContactRegistry.ContactReport.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RabbitMQEventBus.Core;
using RabbitMQEventBus.Events;
using RabbitMQEventBus.Producer;
using System.Net;

namespace ContactRegistry.ContactReport.Controllers;

[Route("api")]
[ApiController]
public class ReportsController : ControllerBase
{

    private readonly IReportRepository _reportService;
    private readonly IRabbitMQEventBusProducer _eventBus;

    public ReportsController(IReportRepository reportService, IRabbitMQEventBusProducer eventBus)
    {
        _reportService = reportService;
        _eventBus=eventBus;
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

    [HttpGet("report-datails")]
    public async Task<IActionResult> GetReportDetails()
    {
        var result = await _reportService.GetReportDetailsAsync();
        return Ok(result);
    }

    [HttpGet("report-datails/{reportId}")]
    public async Task<IActionResult> GetReportDetailsByReportId(string reportId)
    {
        var result = await _reportService.GetReportDetailsByReportIdAsync(reportId);
        return Ok(result);
    }

    [HttpPost("reports")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateReport()
    {
        var result = await _reportService.CreateReportAsync();

        if (result is not null)
        {
            try
            {
                var reportRequest = new ReportRequestEvent() { ReportId = result.Id };
                _eventBus.Publish(EventConstants.ContactReportQueue, reportRequest);
            }
            catch (Exception)
            {
                return BadRequest();

            }
        }

        return Ok(result);
    }


}