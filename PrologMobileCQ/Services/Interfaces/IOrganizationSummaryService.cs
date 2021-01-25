using PrologMobileCQ.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrologMobileCQ.Services.Interfaces
{
    public interface IOrganizationSummaryService
    {
        Task<List<T>> DeserializeDataIntoListOfClass<T>(string endpoint);
        Task ReturnASummaryForEachOrganization();
    }
}
