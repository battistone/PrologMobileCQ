using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
            var services = new ServiceCollection()
            .AddLogging(config => config.AddConsole())      // can add any logger(s)
            .BuildServiceProvider();
            using (var loggerFactory = services.GetRequiredService<ILoggerFactory>())
            {
                const int EMPTY = 0;
                var oss = new OrganizationSummaryService(loggerFactory.CreateLogger<OrganizationSummaryService>());
                string authFundEP = CommonEndpointStrings.AuthFundEndpoint;
                // Act
                IList<OrganizationDto> organizations = await oss.DeserializeDataIntoListOfClass<OrganizationDto>(authFundEP);
                // Assert
                Assert.IsTrue(organizations != null);

                long dataCount = organizations.Count;
                Assert.IsTrue(dataCount > EMPTY);
            }

        }
        [Test]
        public async Task IsReturningRegisteredUsersForTheProvidedOrganization()
        {
            // Arrange
            var services = new ServiceCollection()
            .AddLogging(config => config.AddConsole())      // can add any logger(s)
            .BuildServiceProvider();
            using (var loggerFactory = services.GetRequiredService<ILoggerFactory>())
            {
                const int EMPTY = 0;
                var oss = new OrganizationSummaryService(loggerFactory.CreateLogger<OrganizationSummaryService>());
                string regUserEP = CommonEndpointStrings.RegisteredUserEndpoint;
                // Act
                IList<RegisteredUserDto> users = await oss.DeserializeDataIntoListOfClass<RegisteredUserDto>(regUserEP);
                // Assert
                Assert.IsTrue(users != null);

                long dataCount = users.Count;
                Assert.IsTrue(dataCount > EMPTY);
            }
        }
        [Test]
        public async Task IsReturningPhoneInformationForTheProvidedOrganization()
        {
            // Arrange
            var services = new ServiceCollection()
            .AddLogging(config => config.AddConsole())      // can add any logger(s)
            .BuildServiceProvider();
            using (var loggerFactory = services.GetRequiredService<ILoggerFactory>())
            {
                const int EMPTY = 0;
                var oss = new OrganizationSummaryService(loggerFactory.CreateLogger<OrganizationSummaryService>());
                string phoneNumEP = CommonEndpointStrings.PhoneInformationEndpoint;
                // Act
                IList<PhoneInformationDto> phoneInfo = await oss.DeserializeDataIntoListOfClass<PhoneInformationDto>(phoneNumEP);
                // Assert
                Assert.IsTrue(phoneInfo != null);

                long dataCount = phoneInfo.Count;
                Assert.IsTrue(dataCount > EMPTY);
            }
        }
        [Test]
        public async Task AssertGenerateBlacklistTotalWorksAsExpected()
        {
            // Arrange
            var services = new ServiceCollection()
            .AddLogging(config => config.AddConsole())      // can add any logger(s)
            .BuildServiceProvider();
            using (var loggerFactory = services.GetRequiredService<ILoggerFactory>())
            {
                //Arrange
                var oss = new OrganizationSummaryService(loggerFactory.CreateLogger<OrganizationSummaryService>());
                string organizationID = "1";
                string regUserListString = @"[{'id':'1','organizationId':'1','createdAt':'2020-07-14T00:56:47.374Z','name':'Dr. Peter Gleason','email':'Crystel.Tillman65@hotmail.com'},{'id':'7','organizationId':'1','createdAt':'2020-07-14T07:58:34.987Z','name':'Mrs. Ryder Strosin','email':'Trenton.Konopelski95@hotmail.com'},{'id':'13','organizationId':'1','createdAt':'2020-07-14T02:52:48.990Z','name':'Gilberto Schaefer MD','email':'Carlo.Simonis@yahoo.com'},{'id':'19','organizationId':'1','createdAt':'2020-07-13T20:03:51.431Z','name':'Freda Walter','email':'Mitchel.Greenholt74@hotmail.com'},{'id':'25','organizationId':'1','createdAt':'2020-07-14T14:36:04.095Z','name':'Antoinette Doyle','email':'Reanna78@hotmail.com'},{'id':'31','organizationId':'1','createdAt':'2020-07-14T11:40:25.023Z','name':'Ezequiel Boehm','email':'Benton23@gmail.com'},{'id':'37','organizationId':'1','createdAt':'2020-07-14T08:17:29.971Z','name':'Bartholome Ullrich','email':'Ansley_Crist@yahoo.com'}]";
                string phoneInformationListString = @"[{'id':'1','userId':'1','createdAt':'2020-07-14T01:00:45.801Z','IMEI':32117,'Blacklist':false},{'id':'39','userId':'1','createdAt':'2020-07-13T17:53:47.093Z','IMEI':87092,'Blacklist':true},{'id':'77','userId':'1','createdAt':'2020-07-14T11:31:48.571Z','IMEI':72587,'Blacklist':true}]";
                const string EXPECTED_BLACKLIST_COUNT = "2";
                var regUserList = JsonConvert.DeserializeObject<IList<RegisteredUserDto>>(regUserListString);
                var phoneInformationList = JsonConvert.DeserializeObject<IList<PhoneInformationDto>>(phoneInformationListString);
                // Act
                string blackListCount = oss.GenerateBlacklistTotal(organizationID, regUserList, phoneInformationList);
                // Assert
                // There should be two items on the blacklist
                Assert.IsTrue(blackListCount == EXPECTED_BLACKLIST_COUNT);

            }
        }
    }
}