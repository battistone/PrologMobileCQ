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
        public void AssertGenerateBlacklistTotalWorksAsExpected()
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
                Assert.IsTrue(blackListCount == EXPECTED_BLACKLIST_COUNT);

            }
        }
        [Test]
        public void AssertGenerateTotalCountWorksAsExpected()
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
                const string EXPECTED_TOTAL_NUMBERS_COUNT = "3";
                var regUserList = JsonConvert.DeserializeObject<IList<RegisteredUserDto>>(regUserListString);
                var phoneInformationList = JsonConvert.DeserializeObject<IList<PhoneInformationDto>>(phoneInformationListString);
                // Act
                string totalCount = oss.GenerateTotalCount(organizationID, regUserList, phoneInformationList);
                // Assert
                Assert.IsTrue(totalCount == EXPECTED_TOTAL_NUMBERS_COUNT);

            }
        }
        [Test]
        public void AssertGenerateUsersWorksAsExpected()
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
              
                string expectedJSONOutput = @"[{'id':'1','email':'Crystel.Tillman65@hotmail.com','phoneCount':3}]";
                UserSummaryDto userSummaryDto = new UserSummaryDto
                {
                    ID = "1",
                    PhoneCount = 3,
                    Email = "Crystel.Tillman65@hotmail.com"
                };
                List<UserSummaryDto> uso = new List<UserSummaryDto>
                {
                    userSummaryDto
                };
                // Convert back into JSON for string v. string comparison
                var userSummaryDtosAsJSON = JsonConvert.SerializeObject(uso);
                var regUserList = JsonConvert.DeserializeObject<IList<RegisteredUserDto>>(regUserListString);
                var phoneInformationList = JsonConvert.DeserializeObject<IList<PhoneInformationDto>>(phoneInformationListString);
                var userSummaryDTOExpected = JsonConvert.DeserializeObject<IList<UserSummaryDto>>(expectedJSONOutput);
                // Act
                List<UserSummaryDto> userSummaryDtos = oss.GenerateUsers(organizationID, regUserList, phoneInformationList);
                var responseJSON = JsonConvert.SerializeObject(userSummaryDtos);
                // Assert

                Assert.IsTrue(userSummaryDtosAsJSON == responseJSON);

            }
        }
        [Test]
        public void AssertCreateSummaryForEachOrganizationWorksAsExpected()
        {
            // Arrange
            var services = new ServiceCollection()
            .AddLogging(config => config.AddConsole())      // can add any logger(s)
            .BuildServiceProvider();
            using (var loggerFactory = services.GetRequiredService<ILoggerFactory>())
            {
                //Arrange
                var oss = new OrganizationSummaryService(loggerFactory.CreateLogger<OrganizationSummaryService>());

                string expectedJSONOutput = @"[{""id"":""1"",""name"":""King, Ferry and Harvey"",""blacklistTotal"":""2"",""totalCount"":""3"",""users"":[{""id"":""1"",""email"":""Crystel.Tillman65@hotmail.com"",""phoneCount"":3}]},{""id"":""2"",""name"":""O'Conner - Upton"",""blacklistTotal"":""0"",""totalCount"":""0"",""users"":[]},{""id"":""3"",""name"":""Goodwin and Sons"",""blacklistTotal"":""0"",""totalCount"":""0"",""users"":[]},{""id"":""4"",""name"":""Luettgen LLC"",""blacklistTotal"":""0"",""totalCount"":""0"",""users"":[]},{""id"":""5"",""name"":""Hickle Group"",""blacklistTotal"":""0"",""totalCount"":""0"",""users"":[]},{""id"":""6"",""name"":""Schulist, Reinger and Larson"",""blacklistTotal"":""0"",""totalCount"":""0"",""users"":[]}]";


                string organizationListString = @"[{""id"":""1"",""createdAt"":""2020-07-22T05:04:43.836Z"",""name"":""King, Ferry and Harvey""},{""id"":""2"",""createdAt"":""2020-07-22T04:41:06.264Z"",""name"":""O'Conner - Upton""},{""id"":""3"",""createdAt"":""2020-07-22T19:18:18.352Z"",""name"":""Goodwin and Sons""},{""id"":""4"",""createdAt"":""2020-07-22T16:19:23.182Z"",""name"":""Luettgen LLC""},{""id"":""5"",""createdAt"":""2020-07-22T09:53:54.233Z"",""name"":""Hickle Group""},{""id"":""6"",""createdAt"":""2020-07-21T21:42:34.264Z"",""name"":""Schulist, Reinger and Larson""}]";
                string regUserListString = @"[{'id':'1','organizationId':'1','createdAt':'2020-07-14T00:56:47.374Z','name':'Dr. Peter Gleason','email':'Crystel.Tillman65@hotmail.com'},{'id':'7','organizationId':'1','createdAt':'2020-07-14T07:58:34.987Z','name':'Mrs. Ryder Strosin','email':'Trenton.Konopelski95@hotmail.com'},{'id':'13','organizationId':'1','createdAt':'2020-07-14T02:52:48.990Z','name':'Gilberto Schaefer MD','email':'Carlo.Simonis@yahoo.com'},{'id':'19','organizationId':'1','createdAt':'2020-07-13T20:03:51.431Z','name':'Freda Walter','email':'Mitchel.Greenholt74@hotmail.com'},{'id':'25','organizationId':'1','createdAt':'2020-07-14T14:36:04.095Z','name':'Antoinette Doyle','email':'Reanna78@hotmail.com'},{'id':'31','organizationId':'1','createdAt':'2020-07-14T11:40:25.023Z','name':'Ezequiel Boehm','email':'Benton23@gmail.com'},{'id':'37','organizationId':'1','createdAt':'2020-07-14T08:17:29.971Z','name':'Bartholome Ullrich','email':'Ansley_Crist@yahoo.com'}]";
                string phoneInformationListString = @"[{'id':'1','userId':'1','createdAt':'2020-07-14T01:00:45.801Z','IMEI':32117,'Blacklist':false},{'id':'39','userId':'1','createdAt':'2020-07-13T17:53:47.093Z','IMEI':87092,'Blacklist':true},{'id':'77','userId':'1','createdAt':'2020-07-14T11:31:48.571Z','IMEI':72587,'Blacklist':true}]";


                var orgList = JsonConvert.DeserializeObject<IList<OrganizationDto>>(organizationListString);
                var regUserList = JsonConvert.DeserializeObject<IList<RegisteredUserDto>>(regUserListString);
                var phoneInformationList = JsonConvert.DeserializeObject<IList<PhoneInformationDto>>(phoneInformationListString);
                var userSummaryDTOExpected = JsonConvert.DeserializeObject<IList<UserSummaryDto>>(expectedJSONOutput);
                // Act
                IList<SummaryForEachOrganizationDto> userSummaryEachOrgDtos = oss.CreateSummaryForEachOrganization(orgList, regUserList, phoneInformationList);
                var responseJSON = JsonConvert.SerializeObject(userSummaryEachOrgDtos);
                // Assert

                Assert.IsTrue(expectedJSONOutput.ToUpperInvariant() == responseJSON.ToUpperInvariant());
            }
        }
    }
}