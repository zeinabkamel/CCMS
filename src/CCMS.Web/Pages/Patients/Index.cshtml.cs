using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using CCMS.Permissions;
using CCMS.Patients;

namespace CCMS.Web.Pages.Patients;

[Authorize(CCMSPermissions.Patients.Default)]
public class IndexModel : PageModel
{
    private readonly IPatientAppService _svc;
    public List<PatientDto> Items { get; set; } = new();

    public IndexModel(IPatientAppService svc) => _svc = svc;

    public async Task OnGetAsync()
    {
        var res = await _svc.GetListAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 200 });
        Items = res?.Items?.ToList() ?? new List<PatientDto>();
    }

    [Authorize(CCMSPermissions.Patients.Create)]
    public async Task<IActionResult> OnPostCreateAsync(CreateUpdatePatientDto input)
    {
        if (!ModelState.IsValid)
        {
            // If validation fails, keep user on the page to show validation messages.
            return Page();
        }

        await _svc.CreateAsync(input);
        return RedirectToPage();
    }

    [Authorize(CCMSPermissions.Patients.Update)]
    public async Task<IActionResult> OnPostEditAsync(Guid id, CreateUpdatePatientDto input)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _svc.UpdateAsync(id, input);
        return RedirectToPage();
    }

    [Authorize(CCMSPermissions.Patients.Delete)]
    public async Task<IActionResult> OnPostDeleteAsync(Guid id)
    {
        await _svc.DeleteAsync(id);
        return RedirectToPage();
    }
}
