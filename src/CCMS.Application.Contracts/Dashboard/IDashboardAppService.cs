
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace CCMS.Dashboard;

public interface IDashboardAppService : IApplicationService
{
    Task<DashboardSummaryDto> GetAsync(DashboardFilterInput input);
}
