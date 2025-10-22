using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using CCMS.Permissions;

namespace CCMS.Patients;

public class PatientAppService :
    CrudAppService<Patient, PatientDto, Guid, PagedAndSortedResultRequestDto, CreateUpdatePatientDto>,
    IPatientAppService
{
    public PatientAppService(IRepository<Patient, Guid> repository) : base(repository)
    {
        GetPolicyName = CCMSPermissions.Patients.Default;
        GetListPolicyName = CCMSPermissions.Patients.Default;
        CreatePolicyName = CCMSPermissions.Patients.Create;
        UpdatePolicyName = CCMSPermissions.Patients.Update;
        DeletePolicyName = CCMSPermissions.Patients.Delete;
    }
}

public interface IPatientAppService :
    ICrudAppService<PatientDto, Guid, PagedAndSortedResultRequestDto, CreateUpdatePatientDto> { }
