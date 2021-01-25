using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrologMobileCQ.Controllers
{
    public class OrganizationSummaryController : Controller
    {
        public IActionResult PhoneBreakdownReport()
        {
            return View();
        }
    }
}
