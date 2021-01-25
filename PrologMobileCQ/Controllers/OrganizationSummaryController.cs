using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrologMobileCQ.Models.DTOs;
using PrologMobileCQ.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrologMobileCQ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationSummaryController : Controller
    {
        private readonly IOrganizationSummaryService _organizationSummaryService;
        private readonly ILogger<OrganizationSummaryController> _logger;
        public OrganizationSummaryController(IOrganizationSummaryService organizationSummaryService, ILogger<OrganizationSummaryController> logger)
        {
            _organizationSummaryService = organizationSummaryService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IList<SummaryForEachOrganizationDto>>> Get()
        {
            try
            {
                IList<SummaryForEachOrganizationDto> summaries = await _organizationSummaryService.ReturnASummaryForEachOrganization();
                return Ok(summaries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.InnerException.Message.ToString());
                return BadRequest(ex.InnerException.Message.ToString());
            }
        }
    }
}
