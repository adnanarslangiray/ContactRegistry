using ContactRegistry.ContactReport.Controllers;
using ContactRegistry.ContactReport.Entities;
using ContactRegistry.ContactReport.Helpers;
using ContactRegistry.ContactReport.Repositories.Interfaces;
using FluentAssertions;
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
    private readonly Mock<IReportCreateHelper> _reportCreateHelper;
    private const string PreparingReportId = "6009cb85e65f6dce28fb3e51";
    private const string ComplatedReportId = "507f1f77bcf86cd799439011";

    public ContactReportsTest()
    {
        _reportRepositoryMock= new Mock<IReportRepository>();
        _eventBus =new Mock<IRabbitMQEventBusProducer>();
        _reportCreateHelper = new Mock<IReportCreateHelper>();
        _reportsController = new ReportsController(_reportRepositoryMock.Object, _eventBus.Object, _reportCreateHelper.Object);
    }

    //getAllReports
    [Fact]
    public async Task GetAllReports_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        _reportRepositoryMock.Setup(_ => _.GetReportsAsync())
          .ReturnsAsync(GetMockReportList());

        // Act
        var result = await _reportsController.GetReports();
        // Assert
        result.Should().BeOfType<OkObjectResult>();
        (result as OkObjectResult).StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetAllReports_WhenCalled_ReturnsNoContentResult()
    {
        // Arrange
        _reportRepositoryMock.Setup(x => x.GetReportsAsync())
          .ReturnsAsync(GetMockReportEmptyList());
        // Act
        var result = await _reportsController.GetReports();
        // Assert
        result.Should().BeOfType<NoContentResult>();
        (result as NoContentResult).StatusCode.Should().Be(204);
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
        var objResult = (OkObjectResult)result;
        var reportResult = (Report)objResult.Value;
        Assert.IsType<Report>(objResult.Value);
        Assert.Equal(objResult.StatusCode, (int)HttpStatusCode.OK);
        Assert.Equal(reportResult.Id, ComplatedReportId);
    }
    [Fact]//badrequest
    public async Task CreateReport_WhenCalled_ReturnsBadRequestResult()
    {
        // Arrange
        _reportRepositoryMock.Setup(x => x.CreateReportAsync())
          .Returns(Task.FromResult((Report)null));
        // Act
        var result = await _reportsController.CreateReport();
        // Assert
        result.Should().BeOfType<BadRequestResult>();
        (result as BadRequestResult).StatusCode.Should().Be(400);
    }


    [Fact]
    public async Task GetReportById_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        _reportRepositoryMock.Setup(x => x.GetReportByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(GetMockReportById(ComplatedReportId));
        // Act
        var result = await _reportsController.GetReportById(ComplatedReportId);
        // result is not null
        var eportResult = (Report)((OkObjectResult)result).Value;
        Assert.NotNull(eportResult);
    }
    //nocontent
    [Fact]
    public async Task GetReportById_WhenCalled_ReturnsNotFoundResult()
    {
        // Arrange
        _reportRepositoryMock.Setup(x => x.GetReportByIdAsync(It.IsAny<string>()))
            .ReturnsAsync((Report)null);
        // Act
        var result = await _reportsController.GetReportById(ComplatedReportId);
        // Assert
        result.Should().BeOfType<NotFoundResult>();
        (result as NotFoundResult).StatusCode.Should().Be(404);
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
        var objResult = (OkObjectResult)result;
        var reportResult = (IList<ReportDetail>)objResult.Value;
        Assert.NotNull(reportResult);
    }
    //nocontent
    [Fact]
    public async Task GetReportDetails_WhenCalled_ReturnsNoContentResult()
    {
        IList<ReportDetail> emptyList = new List<ReportDetail>();
        // Arrange
        _reportRepositoryMock.Setup(x => x.GetReportDetailsAsync())
          .Returns(Task.FromResult(emptyList));;
        // Act
        var result = await _reportsController.GetReportDetails();
        // Assert
        result.Should().BeOfType<NoContentResult>();
        (result as NoContentResult).StatusCode.Should().Be(204);
    }



    #region MockData

    //Mock  Data
    private Report GetMockReportById(string id)
    {
        var result = GetMockReportList().FirstOrDefault(x => x.Id == id);
        return result;
    }

    private static IList<Report> GetMockReportList()
    {
        return new List<Report>
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
        };
    }

    private static IList<Report> GetMockReportEmptyList()
    {
        return new List<Report>();
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
    #endregion
}