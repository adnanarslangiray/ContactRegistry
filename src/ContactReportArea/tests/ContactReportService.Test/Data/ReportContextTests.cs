using ContactRegistry.ContactReport.Data;
using ContactRegistry.ContactReport.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using System;
using Xunit;

namespace ContactReportService.Test.Data
{
    public class ReportContextTests
    {
        private MockRepository mockRepository;

        private Mock<IOptions<IContactReportDatabaseSetting>> _mockOptions;

        private Mock<IMongoDatabase> _mockDB;

        private Mock<IMongoClient> _mockClient;

        public ReportContextTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            _mockOptions = new Mock<IOptions<IContactReportDatabaseSetting>>();
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
        }

        private static ContactReportDatabaseSetting GetSetting()
        {
            return new ContactReportDatabaseSetting()
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "contactreportdb",
                CollectionName = "Reports",
                ContactReportCollectionName = "ReportDetails"
            };
        }

        [Fact]
        public void MongoDBContext_Constructor_Success()
        {
            // Arrange
            _mockOptions.Setup(s => s.Value).Returns(GetSetting());
            _mockClient.Setup(c => c
            .GetDatabase(_mockOptions.Object.Value.DatabaseName, null))
                .Returns(_mockDB.Object);

            //Act
            var context = new ReportContext(_mockOptions.Object.Value);

            //Assert
            Assert.NotNull(context);
        }
    }
}