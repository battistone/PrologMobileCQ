using Newtonsoft.Json;
using PrologMobileCQ.Models.DTOs;
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
            var authFundOrgs = DeserializeDataIntoListOfClass<OrganizationDto>(authFundAPI);
         //   var regUserOrgs = 
        }
    }
}
