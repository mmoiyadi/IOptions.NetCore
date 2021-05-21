using Microsoft.AspNetCore.Mvc;
using Options.NetCore.Models;
using Options.NetCore.Services.Interfaces;

namespace Options.NetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reportService;
        private readonly IEmailService emailService;


        public ReportController(IReportService reportService, 
                                IEmailService emailService)
        {
            this.reportService = reportService;
            this.emailService = emailService;
        }

        [HttpPost]
        public IActionResult GenerateAndSendReport(ReportInputModel reportInputModel)
        {
            var report = reportService.GenerateReport(reportInputModel);
            if(report is null)
            {
                return NotFound();
            }
            emailService.Send(report);
            return Ok();
        }
    }
}
