using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Staffing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.Application.Dtos;

namespace CCMS.Web.Pages.Staff;

[Authorize(CCMS.Permissions.CCMSPermissions.Staff.Default)]
public class IndexModel : PageModel
{
    private readonly IStaffAppService _svc;
    public List<StaffDto> Items { get; set; } = new();

    public IndexModel(IStaffAppService svc) => _svc = svc;

    public async Task OnGetAsync()
    {
        var res = await _svc.GetListAsync(new PagedAndSortedResultRequestDto{ MaxResultCount = 500 });
        Items = res.Items?.ToList() ?? new();
    }

    public async Task<IActionResult> OnPostCreateAsync(CreateUpdateStaffDto input)
    {
        await _svc.CreateAsync(input);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEditAsync(Guid id, CreateUpdateStaffDto input)
    {
        await _svc.UpdateAsync(id, input);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(Guid id)
    {
        await _svc.DeleteAsync(id);
        return RedirectToPage();
    }
}
