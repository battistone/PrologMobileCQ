using PrologMobileCQ.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrologMobileCQ.Services.Interfaces
{
    public interface IOrganizationSummaryService
    {
        Task<IList<T>> DeserializeDataIntoListOfClass<T>(string endpoint);
        Task<IList<SummaryForEachOrganizationDto>> ReturnASummaryForEachOrganization();
        IList<SummaryForEachOrganizationDto> CreateSummaryForEachOrganization(IList<OrganizationDto> organizationList, IList<RegisteredUserDto> regUserList, IList<PhoneInformationDto> phoneInformationList);
        string GenerateBlacklistTotal(string organizationID, IList<RegisteredUserDto> regUserList, IList<PhoneInformationDto> phoneInformationList);
        string GenerateTotalCount(string organizationID, IList<RegisteredUserDto> regUserList, IList<PhoneInformationDto> phoneInformationList);
        List<UserSummaryDto> GenerateUsers(string organizationID, IList<RegisteredUserDto> regUserList, IList<PhoneInformationDto> phoneInformationList);
    }
}
