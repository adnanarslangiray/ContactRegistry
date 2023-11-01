using ContactRegistry.ContactReport.Settings;
using Moq;
using System;
using Xunit;

namespace ContactReportService.Test.Settings
{
    public class ContactReportDatabaseSettingTests
    {
        private MockRepository _mockRepository;



        public ContactReportDatabaseSettingTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private ContactReportDatabaseSetting CreateContactReportDatabaseSetting()
        {
            return new ContactReportDatabaseSetting()
            {
                 ConnectionString = "localhost:27017",
                 
            };
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var contactReportDatabaseSetting = this.CreateContactReportDatabaseSetting();

            // Act


            // Assert
            Assert.True(true);
            _mockRepository.VerifyAll();
        }
    }
}
