using NUnit.Framework;
using PrologMobileCQ.Models.DTOs;
using PrologMobileCQ.Services.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrologMobileCQ.Tests
{
    public class TestIntegrationOrganizationSummaryService
    {
        [Test]
        public async Task IsReturningAllOrganizationsWithAuthorizedFundsInTheSystem()
        {
            // Arrange
            const int EMPTY = 0;
            var oss = new OrganizationSummaryService();
            // Act
            List<OrganizationDto> organizations = await oss.DeserializeDataIntoListOfClass<OrganizationDto>("https://5f0ddbee704cdf0016eaea16.mockapi.io/organizations");
            // Assert
            Assert.IsTrue(organizations != null);

            long dataCount = organizations.Count;
            Assert.IsTrue(dataCount > EMPTY);
        }
        [Test]
        public async Task IsReturningRegisteredUsersForTheProvidedOrganization()
        {
            // Arrange
            const int EMPTY = 0;
            var oss = new OrganizationSummaryService();
            // Act
            List<RegisteredUserDto> users = await oss.DeserializeDataIntoListOfClass<RegisteredUserDto>("https://5f0ddbee704cdf0016eaea16.mockapi.io/organizations/1/users");
            // Assert
            Assert.IsTrue(users != null);

            long dataCount = users.Count;
            Assert.IsTrue(dataCount > EMPTY);
        }
        [Test]
        public async Task IsReturningPhoneInformationForTheProvidedOrganization()
        {
            // Arrange
            const int EMPTY = 0;
            var oss = new OrganizationSummaryService();
            // Act
            List<PhoneInformationDto> phoneInfo = await oss.DeserializeDataIntoListOfClass<PhoneInformationDto>("https://5f0ddbee704cdf0016eaea16.mockapi.io/organizations/1/users/1/phones");
            // Assert
            Assert.IsTrue(phoneInfo != null);

            long dataCount = phoneInfo.Count;
            Assert.IsTrue(dataCount > EMPTY);
        }
    }
}