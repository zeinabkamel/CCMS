using AutoMapper;
using CCMS.Staffing;

namespace CCMS;

public class StaffingApplicationAutoMapperProfile : Profile
{
    public StaffingApplicationAutoMapperProfile()
    {
        CreateMap<Staff, StaffDto>();
        CreateMap<CreateUpdateStaffDto, Staff>();

        CreateMap<DoctorProfile, DoctorProfileDto>();
        CreateMap<CreateUpdateDoctorProfileDto, DoctorProfile>();
    }
}
