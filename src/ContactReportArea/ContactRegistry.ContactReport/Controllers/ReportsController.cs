using ContactRegistry.ContactReport.DTOs;
using ContactRegistry.ContactReport.Helpers;
using ContactRegistry.ContactReport.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RabbitMQEventBus.Constants;
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
    private readonly ReportCreateHelper _reportCreateHelper;

    public ReportsController(IReportRepository reportService, IRabbitMQEventBusProducer eventBus, ReportCreateHelper reportCreateHelper)
    {
        _reportService = reportService;
        _eventBus=eventBus;
        _reportCreateHelper=reportCreateHelper;
    }

    [HttpGet("reports")]
    public async Task<IActionResult> GetReports()
    {
        var result = await _reportService.GetReportsAsync();
        if (result?.Count == 0)
            return NoContent();

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
    [HttpPost("create-report-details")]
    public async Task<IActionResult> CreateReportDetails(ContactReportCreateDto contactReport)
    {
        await _reportCreateHelper.ContactReportProcess(contactReport);

        return Ok();
    }

}