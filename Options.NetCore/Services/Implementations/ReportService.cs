using Options.NetCore.Models;
using Options.NetCore.Services.Interfaces;

namespace Options.NetCore.Services
{
    public class ReportService : IReportService
    {
        public string GenerateReport(ReportInputModel reportInputModel)
        {
            //throw new Exception("Exception in generating report");
            return $"Report generated for report id: {reportInputModel}";
        }
    }
}
