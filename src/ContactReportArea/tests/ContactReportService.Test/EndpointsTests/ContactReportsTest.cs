using ContactRegistry.Common.Utilities;
using ContactRegistry.ContactReport.Controllers;
using ContactRegistry.ContactReport.Entities;
using ContactRegistry.ContactReport.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RabbitMQEventBus.Producer;
using System.Net;

namespace ContactReportService.Test.EndpointsTests;

public class ContactReportsTest
{
    private readonly Mock<IReportRepository> _reportRepositoryMock;
    private readonly ReportsController _reportsController;
    private readonly Mock<IRabbitMQEventBusProducer> _eventBus;
    private const string PreparingReportId = "6009cb85e65f6dce28fb3e51";
    private const string ComplatedReportId = "507f1f77bcf86cd799439011";
    public ContactReportsTest()
    {
        _reportRepositoryMock = new();
        _eventBus =new Mock<IRabbitMQEventBusProducer>();
        _reportsController = new ReportsController(_reportRepositoryMock.Object, _eventBus.Object);
        _reportRepositoryMock= new Mock<IReportRepository>();
    }

    //getAllReports
    [Fact]
    public async Task GetAllReports_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        _reportRepositoryMock.Setup(x => x.GetReportsAsync())
          .Returns(Task.FromResult(GetMockReportList()));
        // Act
        var result = await _reportsController.GetReports();
        // Assert
        Assert.IsType<BaseResponse<IList<Report>>>(((ObjectResult)result).Value);
    }

    [Fact]//createReport
    public async Task CreateReport_WhenCalled_ReturnsOkResult()
    {
        var report = GetMockReportById(ComplatedReportId);
        // Arrange
        _reportRepositoryMock.Setup(x => x.CreateReportAsync())
          .Returns(Task.FromResult(report));
        // Act
        var result = await _reportsController.CreateReport();
        // Assert
        var objResult = (ObjectResult)result;
        var reportResult = (Report)objResult.Value;
        Assert.IsType<Report>(objResult.Value);
        Assert.Equal(objResult.StatusCode, (int)HttpStatusCode.OK);
        Assert.NotNull(reportResult);
        Assert.Equal(reportResult.Id, ComplatedReportId);
    }


    [Fact]
    public async Task GetReportById_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        _reportRepositoryMock.Setup(x => x.GetReportByIdAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(GetMockReportById(ComplatedReportId)));
        // Act
        var result = await _reportsController.GetReportById(id);
        // result is not null
        Assert.NotNull(result);
    }



    [Fact]
    public async Task GetReportDetails_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        _reportRepositoryMock.Setup(x => x.GetReportDetailsAsync())
          .Returns(Task.FromResult((GetMockReportDetailsList())));
        // Act
        var result = await _reportsController.GetReportDetails();
        // Assert
        Assert.IsType<IList<ReportDetail>>(((ObjectResult)result).Value);
    }

    private Report GetMockReportById(string id)
    {
        var result = GetMockReportList().Data.FirstOrDefault(x => x.Id == id);
        return result;
    }

    private static BaseResponse<IList<Report>> GetMockReportList()
    {
        return new BaseResponse<IList<Report>>(new List<Report>
        {
            new Report
            {
                Id = PreparingReportId,
                Status = Report.ReportStatus.Preparing,
            },
            new Report
            {
                Id = ComplatedReportId,
                Status = Report.ReportStatus.Completed,
            }
        }, true);
    }

    private static IList<ReportDetail> GetMockReportDetailsList()
    {
        return new List<ReportDetail>
        {
            new ReportDetail
            {
                Id = new Guid().ToString(),
                ContactCount = 1,
                CreatedDate = DateTime.Now,
                ReportId = ComplatedReportId,
                Location = "test",
                PhoneNumberCount = 1,
                UpdatedDate = DateTime.Now
            },
            new ReportDetail
            {
                Id = new Guid().ToString(),
                ContactCount = 2,
                CreatedDate = DateTime.Now,
                ReportId = PreparingReportId,
                Location = "test",
                PhoneNumberCount = 2,
                UpdatedDate = DateTime.Now
            }
        };
    }
}