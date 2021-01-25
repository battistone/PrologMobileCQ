using Microsoft.Extensions.Logging;
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
        private readonly ILogger<OrganizationSummaryService> _logger;
        public OrganizationSummaryService(ILogger<OrganizationSummaryService> logger)
        {
            _logger = logger;
        }
        public async Task<IList<T>> DeserializeDataIntoListOfClass<T>(string endpoint)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                _logger.LogError(ex.InnerException.Message.ToString());
                throw;
            }
        }
        public async Task<IList<SummaryForEachOrganizationDto>> ReturnASummaryForEachOrganization()
        {
            try
            {
                // Fetch the data.
                string authFundAPI = CommonEndpointStrings.AuthFundEndpoint;
                string regUserAPI = CommonEndpointStrings.RegisteredUserEndpoint;
                string userBlackListAPI = CommonEndpointStrings.PhoneInformationEndpoint;
                var organizationList = await DeserializeDataIntoListOfClass<OrganizationDto>(authFundAPI);
                var regUserList = await DeserializeDataIntoListOfClass<RegisteredUserDto>(regUserAPI);
                var phoneInformationList = await DeserializeDataIntoListOfClass<PhoneInformationDto>(userBlackListAPI);

                return CreateSummaryForEachOrganization(organizationList, regUserList, phoneInformationList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                _logger.LogError(ex.InnerException.Message.ToString());
                throw;
            }
        }
        public IList<SummaryForEachOrganizationDto> CreateSummaryForEachOrganization(IList<OrganizationDto> organizationList, IList<RegisteredUserDto> regUserList, IList<PhoneInformationDto> phoneInformationList)
        {
            try
            {
                IList<SummaryForEachOrganizationDto> summarize = new List<SummaryForEachOrganizationDto>();
                foreach (var org in organizationList)
                {
                    SummaryForEachOrganizationDto tmp = new SummaryForEachOrganizationDto();
                    tmp.ID = org.ID;
                    tmp.Name = org.Name;
                    tmp.BlacklistTotal = GenerateBlacklistTotal(org.ID, regUserList, phoneInformationList);
                    tmp.TotalCount = GenerateTotalCount(org.ID, regUserList, phoneInformationList);
                    tmp.Users = GenerateUsers(org.ID, regUserList, phoneInformationList);

                    summarize.Add(tmp);
                }
                return summarize;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                _logger.LogError(ex.InnerException.Message.ToString());
                throw;
            }
        }
        public string GenerateBlacklistTotal(string organizationID, IList<RegisteredUserDto> regUserList, IList<PhoneInformationDto> phoneInformationList)
        {
            try
            {
                return (from regUser in regUserList
                        join phoneInfo in phoneInformationList on regUser.ID equals phoneInfo.UserID
                        where regUser.OrganizationID == organizationID && phoneInfo.Blacklist == true
                        select new { ID = regUser.ID }).ToList().Count().ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                _logger.LogError(ex.InnerException.Message.ToString());
                throw;
            }
        }
        public string GenerateTotalCount(string organizationID, IList<RegisteredUserDto> regUserList, IList<PhoneInformationDto> phoneInformationList)
        {
            try
            {
                return (from regUser in regUserList
                        join phoneInfo in phoneInformationList on regUser.ID equals phoneInfo.UserID
                        where regUser.OrganizationID == organizationID
                        select new { ID = regUser.ID }).ToList().Count().ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                _logger.LogError(ex.InnerException.Message.ToString());
                throw;
            }
        }
        public List<UserSummaryDto> GenerateUsers(string organizationID, IList<RegisteredUserDto> regUserList, IList<PhoneInformationDto> phoneInformationList)
        {
            try
            {
                return (from regUser in regUserList
                        join phoneInfo in phoneInformationList on regUser.ID equals phoneInfo.UserID
                        where regUser.OrganizationID == organizationID
                        // PHONE count is gonna be a subquery
                        select new { ID = regUser.ID, Email = regUser.Email, PhoneIMEI = phoneInfo.IMEI })
                                 .ToList().GroupBy(a => new { a.ID, a.Email })
                                 .Select(x => new UserSummaryDto
                                 {
                                     ID = x.Key.ID,
                                     Email = x.Key.Email,
                                     PhoneCount = x.Select(z => z.PhoneIMEI).Distinct().Count()
                                 }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                _logger.LogError(ex.InnerException.Message.ToString());
                throw;
            }
        }
    }
}
