using NUnit.Framework;
using PrologMobileCQ.Models.DTOs;
using PrologMobileCQ.Services.Classes;
using PrologMobileCQ.Util;
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
            string authFundEP = CommonEndpointStrings.AuthFundEndpoint;
            // Act
            IList<OrganizationDto> organizations = await oss.DeserializeDataIntoListOfClass<OrganizationDto>(authFundEP);
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
            string regUserEP = CommonEndpointStrings.RegisteredUserEndpoint;
            // Act
            IList<RegisteredUserDto> users = await oss.DeserializeDataIntoListOfClass<RegisteredUserDto>(regUserEP);
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
            string phoneNumEP = CommonEndpointStrings.PhoneInformationEndpoint;
            // Act
            IList<PhoneInformationDto> phoneInfo = await oss.DeserializeDataIntoListOfClass<PhoneInformationDto>(phoneNumEP);
            // Assert
            Assert.IsTrue(phoneInfo != null);

            long dataCount = phoneInfo.Count;
            Assert.IsTrue(dataCount > EMPTY);
        }
        [Test]
        public async Task AssertEndpointWorks()
        {
            // Arrange
            const int EMPTY = 0;
            var oss = new OrganizationSummaryService();
            string phoneNumEP = CommonEndpointStrings.PhoneInformationEndpoint;
            // Act
            IList<PhoneInformationDto> phoneInfo = await oss.DeserializeDataIntoListOfClass<PhoneInformationDto>(phoneNumEP);
            // Assert
            Assert.IsTrue(phoneInfo != null);

            long dataCount = phoneInfo.Count;
            Assert.IsTrue(dataCount > EMPTY);
        }
    }
}