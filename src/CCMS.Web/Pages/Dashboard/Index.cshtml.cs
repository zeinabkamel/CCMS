using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using CCMS.Dashboard;
using CCMS.Staffing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.Application.Dtos;
using ClosedXML.Excel; // dotnet add src/CCMS.Web/CCMS.Web.csproj package ClosedXML

namespace CCMS.Web.Pages.Dashboard;

[Authorize(CCMS.Permissions.CCMSPermissions.Dashboard.Default)]
public class IndexModel : PageModel
{
    private readonly IDashboardAppService _svc;
    private readonly IStaffAppService _staff;

    public DashboardSummaryVm Vm { get; set; } = new();
    public List<StaffDto> Doctors { get; set; } = new();

    [BindProperty(SupportsGet = true)] public DateTime? From { get; set; }
    [BindProperty(SupportsGet = true)] public DateTime? To { get; set; }
    [BindProperty(SupportsGet = true)] public Guid? DoctorStaffId { get; set; }
    [BindProperty(SupportsGet = true)] public bool OnlyMine { get; set; }

    public IndexModel(IDashboardAppService svc, IStaffAppService staff)
    {
        _svc = svc;
        _staff = staff;
    }

    public async Task OnGetAsync()
    {
        await LoadDoctorsAsync();
        var data = await _svc.GetAsync(new DashboardFilterInput
        {
            From = From, To = To, DoctorStaffId = DoctorStaffId, OnlyMine = OnlyMine
        });

        Vm = new DashboardSummaryVm
        {
            From = data.From,
            To = data.To,
            TotalSessions = data.TotalSessions,
            MySessions = data.MySessions,
            CompletedSessions = data.CompletedSessions,
            CanceledSessions = data.CanceledSessions,
            AvgSessionsPerDoctor = data.AvgSessionsPerDoctor,
            MaterialsLabels = data.TopMaterials.Select(x => x.Name).ToArray(),
            MaterialsValues = data.TopMaterials.Select(x => (double)x.QtyUsed).ToArray(),
            DoctorsLabels = data.SessionsPerDoctor.Select(x => x.DoctorName).ToArray(),
            DoctorsValues = data.SessionsPerDoctor.Select(x => (double)x.Count).ToArray()
        };

        Vm.MaxDoctorSessions = Vm.DoctorsValues.Any() ? Vm.DoctorsValues.Max() : 0;
    }

    public async Task<FileResult> OnGetExportCsvAsync()
    {
        var data = await _svc.GetAsync(new DashboardFilterInput
        {
            From = From, To = To, DoctorStaffId = DoctorStaffId, OnlyMine = OnlyMine
        });

        var sb = new StringBuilder();
        sb.AppendLine("Doctor,Count");
        foreach (var d in data.SessionsPerDoctor)
            sb.AppendLine($"\"{d.DoctorName.Replace("\"", "\"\"")}\",{d.Count}");

        sb.AppendLine();
        sb.AppendLine("RawMaterial,QtyUsed");
        foreach (var m in data.TopMaterials)
            sb.AppendLine($"\"{m.Name.Replace("\"", "\"\"")}\",{m.QtyUsed}");

        var bytes = Encoding.UTF8.GetBytes(sb.ToString());
        var name = $"dashboard_{DateTime.UtcNow:yyyyMMddHHmmss}.csv";
        return File(bytes, "text/csv", name);
    }

    public async Task<FileResult> OnGetExportExcelAsync()
    {
        var data = await _svc.GetAsync(new DashboardFilterInput
        {
            From = From, To = To, DoctorStaffId = DoctorStaffId, OnlyMine = OnlyMine
        });

        using var wb = new ClosedXML.Excel.XLWorkbook();
        var ws1 = wb.AddWorksheet("SessionsPerDoctor");
        ws1.Cell(1,1).Value = "Doctor";
        ws1.Cell(1,2).Value = "Count";
        for (int i = 0; i < data.SessionsPerDoctor.Count; i++)
        {
            ws1.Cell(i+2,1).Value = data.SessionsPerDoctor[i].DoctorName;
            ws1.Cell(i+2,2).Value = data.SessionsPerDoctor[i].Count;
        }
        ws1.Columns().AdjustToContents();

        var ws2 = wb.AddWorksheet("TopMaterials");
        ws2.Cell(1,1).Value = "RawMaterial";
        ws2.Cell(1,2).Value = "QtyUsed";
        for (int i = 0; i < data.TopMaterials.Count; i++)
        {
            ws2.Cell(i+2,1).Value = data.TopMaterials[i].Name;
            ws2.Cell(i+2,2).Value = (double)data.TopMaterials[i].QtyUsed;
        }
        ws2.Columns().AdjustToContents();

        var ws3 = wb.AddWorksheet("Summary");
        ws3.Cell(1,1).Value = "From"; ws3.Cell(1,2).Value = data.From;
        ws3.Cell(2,1).Value = "To"; ws3.Cell(2,2).Value = data.To;
        ws3.Cell(4,1).Value = "TotalSessions"; ws3.Cell(4,2).Value = data.TotalSessions;
        ws3.Cell(5,1).Value = "Completed"; ws3.Cell(5,2).Value = data.CompletedSessions;
        ws3.Cell(6,1).Value = "Canceled"; ws3.Cell(6,2).Value = data.CanceledSessions;
        ws3.Cell(7,1).Value = "AvgPerDoctor"; ws3.Cell(7,2).Value = data.AvgSessionsPerDoctor;
        ws3.Columns().AdjustToContents();

        using var ms = new System.IO.MemoryStream();
        wb.SaveAs(ms);
        var bytes = ms.ToArray();
        var name = $"dashboard_{DateTime.UtcNow:yyyyMMddHHmmss}.xlsx";
        return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", name);
    }

    private async Task LoadDoctorsAsync()
    {
        var res = await _staff.GetListAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 1000 });
        Doctors = (res.Items ?? Array.Empty<StaffDto>()).Where(x => x.Role == (int)CCMS.Enums.StaffRole.Doctor).ToList();
    }
}

public class DashboardSummaryVm
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int TotalSessions { get; set; }
    public int MySessions { get; set; }
    public int CompletedSessions { get; set; }
    public int CanceledSessions { get; set; }
    public double AvgSessionsPerDoctor { get; set; }
    public string[] MaterialsLabels { get; set; } = Array.Empty<string>();
    public double[] MaterialsValues { get; set; } = Array.Empty<double>();
    public string[] DoctorsLabels { get; set; } = Array.Empty<string>();
    public double[] DoctorsValues { get; set; } = Array.Empty<double>();
    public double MaxDoctorSessions { get; set; }
}
