using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CCMS.Staffing;

public interface IStaffAppService :
    ICrudAppService<StaffDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateStaffDto>
{ }

public interface IDoctorProfileAppService :
    ICrudAppService<DoctorProfileDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateDoctorProfileDto>
{ }
