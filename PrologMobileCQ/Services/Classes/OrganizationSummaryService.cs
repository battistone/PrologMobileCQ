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
        public async Task<IList<T>> DeserializeDataIntoListOfClass<T>(string endpoint)
        {
            IList<T> organizationList = new List<T>();
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
        public async Task<IList<SummaryForEachOrganizationDto>> ReturnASummaryForEachOrganization()
        {
            // Fetch the data.
            string authFundAPI = CommonEndpointStrings.AuthFundEndpoint;
            string regUserAPI = CommonEndpointStrings.RegisteredUserEndpoint;
            string userBlackListAPI = CommonEndpointStrings.PhoneInformationEndpoint;
            var organizationList = await DeserializeDataIntoListOfClass<OrganizationDto>(authFundAPI);
            var regUserList = await DeserializeDataIntoListOfClass<RegisteredUserDto>(regUserAPI);
            var phoneInformationList = await DeserializeDataIntoListOfClass<PhoneInformationDto>(userBlackListAPI);

            IList<SummaryForEachOrganizationDto> summarize = new List<SummaryForEachOrganizationDto>();
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
                tmp.Users = 
                               
                             (from regUser in regUserList
                             join phoneInfo in phoneInformationList on regUser.ID equals phoneInfo.UserID
                             where regUser.OrganizationID == org.ID
                             // PHONE count is gonna be a subquery
                             select new { ID = regUser.ID, Email = regUser.Email, PhoneIMEI = phoneInfo.IMEI })
                             .ToList().GroupBy(a => new { a.ID, a.Email })
                             .Select(x => new UserSummaryDto
                             {
                                 ID = x.Key.ID,
                                 Email = x.Key.Email,
                                 PhoneCount = x.Select(z => z.PhoneIMEI).Distinct().Count()
                             }).ToList();


                summarize.Add(tmp);
            }
            return summarize;
        }
    }
}
