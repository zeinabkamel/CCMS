using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Sessions;
using CCMS.Staffing;
using CCMS.Patients;
using CCMS.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.Application.Dtos;

namespace CCMS.Web.Pages.Sessions;

[Authorize(CCMS.Permissions.CCMSPermissions.Sessions.Default)]
public class IndexModel : PageModel
{
    private readonly ISessionAppService _sessions;
    private readonly IStaffAppService _staff;
    private readonly IPatientAppService _patients;
    private readonly IRawMaterialAppService _raw;

    public List<SessionDto> Items { get; set; } = new();
    public List<StaffDto> Doctors { get; set; } = new();
    public List<PatientDto> Patients { get; set; } = new();
    public List<RawMaterialDto> RawMaterials { get; set; } = new();

    public IndexModel(ISessionAppService sessions, IStaffAppService staff, IPatientAppService patients, IRawMaterialAppService raw)
    {
        _sessions = sessions; _staff = staff; _patients = patients; _raw = raw;
    }

    public async Task OnGetAsync()
    {
        var res = await _sessions.GetListAsync(new PagedAndSortedResultRequestDto{ MaxResultCount = 500});
        Items = res.Items?.ToList() ?? new();

        var staffRes = await _staff.GetListAsync(new PagedAndSortedResultRequestDto{ MaxResultCount = 500});
        Doctors = (staffRes.Items ?? Array.Empty<StaffDto>()).Where(x => x.Role == (int)Enums.StaffRole.Doctor).ToList();

        var pat = await _patients.GetListAsync(new PagedAndSortedResultRequestDto{ MaxResultCount = 500});
        Patients = pat.Items?.ToList() ?? new();

        var rm = await _raw.GetListAsync(new PagedAndSortedResultRequestDto{ MaxResultCount = 500});
        RawMaterials = rm.Items?.ToList() ?? new();
    }

    public string PatientLookup(Guid id) => Patients.FirstOrDefault(x => x.Id == id)?.FullName ?? id.ToString();
    public string DoctorLookup(Guid id)  => Doctors.FirstOrDefault(x => x.Id == id )?.FullName ?? id.ToString();

    public async Task<IActionResult> OnPostCreateAsync(CreateUpdateSessionDto input)
    {
        await _sessions.CreateAsync(input);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostCompleteAsync(Guid id)
    {
        await _sessions.CompleteAsync(id, null);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(Guid id)
    {
        await _sessions.DeleteAsync(id);
        return RedirectToPage();
    }
}
