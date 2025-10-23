using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Staffing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.Application.Dtos;

namespace CCMS.Web.Pages.DoctorProfiles;

[Authorize(CCMS.Permissions.CCMSPermissions.Doctors.Default)]
public class IndexModel : PageModel
{
    private readonly IDoctorProfileAppService _docs;
    private readonly IStaffAppService _staff;

    public List<DoctorProfileDto> Items { get; set; } = new();
    public List<StaffDto> Staff { get; set; } = new();
    public Dictionary<Guid,string> StaffLookup { get; set; } = new();

    public IndexModel(IDoctorProfileAppService docs, IStaffAppService staff)
    {
        _docs = docs; _staff = staff;
    }

    public async Task OnGetAsync()
    {
        var r1 = await _docs.GetListAsync(new PagedAndSortedResultRequestDto{ MaxResultCount=500});
        Items = r1.Items?.ToList() ?? new();

        var r2 = await _staff.GetListAsync(new PagedAndSortedResultRequestDto{ MaxResultCount=500});
        Staff = r2.Items?.ToList() ?? new();
        StaffLookup = Staff.ToDictionary(x=>x.Id, x=>x.FullName);
    }

    public async Task<IActionResult> OnPostCreateAsync(CreateUpdateDoctorProfileDto input)
    {
        await _docs.CreateAsync(input);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEditAsync(Guid id, CreateUpdateDoctorProfileDto input)
    {
        await _docs.UpdateAsync(id, input);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(Guid id)
    {
        await _docs.DeleteAsync(id);
        return RedirectToPage();
    }
}
