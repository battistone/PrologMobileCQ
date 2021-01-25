using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrologMobileCQ.Models.DTOs
{
    public class SummaryForEachOrganizationDto
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string BlacklistTotal { get; set; }
        public string TotalCount { get; set; }
        public List<UserSummaryDto> Users { get; set; }
    }
}
