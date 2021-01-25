using Newtonsoft.Json;
using PrologMobileCQ.Models.DTOs;
using PrologMobileCQ.Models.Other;
using PrologMobileCQ.Services.Interfaces;
using PrologMobileCQ.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrologMobileCQ.Services.Classes
{
    public class OrganizationSummaryService : IOrganizationSummaryService
    {
        public async Task<List<T>> DeserializeDataIntoListOfClass<T>(string endpoint)
        {
            List<T> organizationList = new List<T>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(endpoint))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    organizationList = JsonConvert.DeserializeObject<List<T>>(apiResponse);
                }
            }
            return organizationList;
        }
        public async Task ReturnASummaryForEachOrganization()
        {
            // Fetch the data.
            string authFundAPI = CommonEndpointStrings.AuthFundEndpoint;
            string regUserAPI = CommonEndpointStrings.RegisteredUserEndpoint;
            string userBlackListAPI = CommonEndpointStrings.PhoneInformationEndpoint;
            var organizationList = await DeserializeDataIntoListOfClass<OrganizationDto>(authFundAPI);
            var regUserList = await DeserializeDataIntoListOfClass<RegisteredUserDto>(regUserAPI);
            var phoneInformationList = await DeserializeDataIntoListOfClass<PhoneInformationDto>(userBlackListAPI);

            List<SummaryForEachOrganizationDto> summarize = new List<SummaryForEachOrganizationDto>();
            foreach (var org in organizationList)
            {
                SummaryForEachOrganizationDto tmp = new SummaryForEachOrganizationDto();
                tmp.ID = org.ID;
                tmp.Name = org.Name;
                tmp.BlacklistTotal = (from regUser in regUserList
                                      join phoneInfo in phoneInformationList on regUser.ID equals phoneInfo.UserID
                                      where regUser.OrganizationID == org.ID && phoneInfo.Blacklist == true
                                      select new { ID = regUser.ID}).ToList().Count().ToString();
                tmp.TotalCount = (from regUser in regUserList
                                  join phoneInfo in phoneInformationList on regUser.ID equals phoneInfo.UserID
                                  where regUser.OrganizationID == org.ID
                                  select new { ID = regUser.ID }).ToList().Count().ToString();
                tmp.Users = (from regUser in regUserList
                             join phoneInfo in phoneInformationList on regUser.ID equals phoneInfo.UserID
                             where regUser.OrganizationID == org.ID
                             // PHONE count is gonna be a subquery
                             select new UserSummaryDto { ID = regUser.ID, Email = regUser.Email, PhoneCount = -1 }).ToList();

                summarize.Add(tmp);
            }
            // Use Linq to join these.
            List<JoinedPhoneData> joinedPhoneData = (from organization in organizationList
                        join regUser in regUserList on organization.ID equals regUser.OrganizationID
                        join phoneInfo in phoneInformationList on regUser.ID equals phoneInfo.UserID
                        select new JoinedPhoneData{ ID = organization.ID, Name = organization.Name, BlacklistState = phoneInfo.Blacklist, RegisteredUser = regUser.Name, RegisteredUserID = regUser.ID, RegisteredUserEmail = regUser.Email, RegisteredUserPhoneIMEI = phoneInfo.IMEI }).ToList();
            var test = "hi";
        }
    }
}
