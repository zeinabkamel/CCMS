using System;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CCMS.Sessions;

public class SessionAppService :
    CrudAppService<Session, SessionDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateSessionDto>,
    ISessionAppService
{
    private readonly ISessionRepository _repo;

    public SessionAppService(ISessionRepository repo) : base(repo)
    {
        _repo = repo;
    }

    protected override string GetPolicyName => CCMSPermissions.Sessions.Default;
    protected override string GetListPolicyName => CCMSPermissions.Sessions.Default;
    protected override string CreatePolicyName => CCMSPermissions.Sessions.Create;
    protected override string UpdatePolicyName => CCMSPermissions.Sessions.Update;
    protected override string DeletePolicyName => CCMSPermissions.Sessions.Delete;

    public override async Task<SessionDto> GetAsync(Guid id)
    {
        var entity = await _repo.GetWithDetailsAsync(id);
        return MapToGetOutputDto(entity);
    }

    public override async Task<SessionDto> CreateAsync(CreateUpdateSessionDto input)
    {
        var entity = new Session(
            GuidGenerator.Create(),
            input.PatientId,
            input.DoctorStaffId,
            (Enums.SessionType)input.Type,
            input.ScheduledAt,
            input.DurationMins
        );

        if (input.Materials != null)
        {
            foreach (var m in input.Materials.Where(x => x.RawMaterialId != Guid.Empty && x.QtyUsed > 0))
            {
                entity.AddMaterial(m.RawMaterialId, m.QtyUsed);
            }
        }

        await Repository.InsertAsync(entity, autoSave: true);
        return MapToGetOutputDto(entity);
    }

    public async Task RescheduleAsync(Guid id, DateTime newDate)
    {
        var e = await Repository.GetAsync(id);
        e.Reschedule(newDate);
        await Repository.UpdateAsync(e, true);
    }

    public async Task CompleteAsync(Guid id, string? notes)
    {
        var e = await Repository.GetAsync(id);
        e.Complete(notes);
        await Repository.UpdateAsync(e, true);
    }

    public async Task CancelAsync(Guid id, string? notes)
    {
        var e = await Repository.GetAsync(id);
        e.Cancel(notes);
        await Repository.UpdateAsync(e, true);
    }
}
