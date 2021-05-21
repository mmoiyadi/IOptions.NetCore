using Options.NetCore.Models;

namespace Options.NetCore.Services.Interfaces
{
    public interface IReportService
    {
        string GenerateReport(ReportInputModel reportInputModel);
    }
}
