
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace CCMS.Dashboard;

public class TopRawMaterialItemDto
{
    public Guid RawMaterialId { get; set; }
    public string Name { get; set; } = "";
    public decimal QtyUsed { get; set; }
}

public class DoctorSessionsCountDto
{
    public Guid DoctorStaffId { get; set; }
    public string DoctorName { get; set; } = "";
    public int Count { get; set; }
}

public class DashboardSummaryDto : EntityDto<Guid>
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }

    public int TotalSessions { get; set; }
    public int MySessions { get; set; }
    public int CompletedSessions { get; set; }
    public int CanceledSessions { get; set; }
    public double AvgSessionsPerDoctor { get; set; }

    public List<DoctorSessionsCountDto> SessionsPerDoctor { get; set; } = new();
    public List<TopRawMaterialItemDto> TopMaterials { get; set; } = new();
}

public class DashboardFilterInput
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public Guid? DoctorStaffId { get; set; }
    public bool OnlyMine { get; set; }
}
