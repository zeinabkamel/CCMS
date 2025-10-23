
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using CCMS.Dashboard;
using CCMS.Sessions;
using CCMS.Staffing;
using CCMS.Stores;
using CCMS.Permissions;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using CCMS.Enums;

namespace CCMS;

[Authorize(CCMSPermissions.Dashboard.Default)]
public class DashboardAppService : ApplicationService, IDashboardAppService
{
    private readonly ISessionRepository _sessions;
    private readonly IRepository<SessionMaterial, Guid> _sessionMaterials;
    private readonly IRepository<RawMaterial, Guid> _raws;
    private readonly IRepository<Staff, Guid> _staff;

    public DashboardAppService(
        ISessionRepository sessions,
        IRepository<SessionMaterial, Guid> sessionMaterials,
        IRepository<RawMaterial, Guid> raws,
        IRepository<Staff, Guid> staff)
    {
        _sessions = sessions;
        _sessionMaterials = sessionMaterials;
        _raws = raws;
        _staff = staff;
    }

    public async Task<DashboardSummaryDto> GetAsync(DashboardFilterInput input)
    {
        var from = (input.From ?? DateTime.Today).Date;
        var toExclusive = ((input.To ?? DateTime.Today).Date).AddDays(1); // exclusive

        var q = (await _sessions.GetQueryableAsync())
            .Where(s => s.ScheduledAt >= from && s.ScheduledAt < toExclusive);

        if (input.DoctorStaffId.HasValue)
            q = q.Where(s => s.DoctorStaffId == input.DoctorStaffId.Value);

        Guid? myStaffId = null;
        if (input.OnlyMine && CurrentUser?.Id != null)
        {
            var me = await (await _staff.GetQueryableAsync())
                .Where(x => x.UserId == CurrentUser.Id)
                .Select(x => new { x.Id })
                .FirstOrDefaultAsync();
            myStaffId = me?.Id;

            if (myStaffId.HasValue)
                q = q.Where(s => s.DoctorStaffId == myStaffId.Value);
        }

        var sessions = await q.ToListAsync();

        // NOTE: adjust enum names if different in your project
        var completed = sessions.Count(s => s.Status == SessionStatus.Completed);
        var canceled  = sessions.Count(s => s.Status == SessionStatus.Canceled);

        var dto = new DashboardSummaryDto
        {
            Id = Guid.NewGuid(),
            From = from,
            To = toExclusive.AddDays(-1),
            TotalSessions = sessions.Count,
            MySessions = myStaffId.HasValue ? sessions.Count(s => s.DoctorStaffId == myStaffId.Value) : 0,
            CompletedSessions = completed,
            CanceledSessions = canceled
        };

        var docIds = sessions.Select(s => s.DoctorStaffId).Distinct().ToList();
        var doctors = await (await _staff.GetQueryableAsync())
            .Where(s => docIds.Contains(s.Id))
            .Select(s => new { s.Id, s.FullName })
            .ToListAsync();

        dto.SessionsPerDoctor = sessions
            .GroupBy(s => s.DoctorStaffId)
            .Select(g => new DoctorSessionsCountDto
            {
                DoctorStaffId = g.Key,
                DoctorName = doctors.FirstOrDefault(d => d.Id == g.Key)?.FullName ?? g.Key.ToString(),
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .ToList();

        dto.AvgSessionsPerDoctor = dto.SessionsPerDoctor.Any()
            ? Math.Round(dto.SessionsPerDoctor.Average(x => x.Count), 2)
            : 0.0;

        var sessIds = sessions.Select(s => s.Id).Distinct().ToList();
        var mats = await (await _sessionMaterials.GetQueryableAsync())
            .Where(m => sessIds.Contains(m.SessionId))
            .ToListAsync();

        var rawIds = mats.Select(m => m.RawMaterialId).Distinct().ToList();
        var rawNames = await (await _raws.GetQueryableAsync())
            .Where(r => rawIds.Contains(r.Id))
            .Select(r => new { r.Id, r.Name })
            .ToListAsync();

        dto.TopMaterials = mats
            .GroupBy(m => m.RawMaterialId)
            .Select(g => new TopRawMaterialItemDto
            {
                RawMaterialId = g.Key,
                Name = rawNames.FirstOrDefault(r => r.Id == g.Key)?.Name ?? g.Key.ToString(),
                QtyUsed = g.Sum(x => x.QtyUsed)
            })
            .OrderByDescending(x => x.QtyUsed)
            .Take(10)
            .ToList();

        return dto;
    }
}
