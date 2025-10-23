using AutoMapper;
using CCMS.Books;
using CCMS.Patients;
using CCMS.Sessions;
using CCMS.Stores;

namespace CCMS;

public class CCMSApplicationAutoMapperProfile : Profile
{
    public CCMSApplicationAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        CreateMap<Patient, PatientDto>();
        CreateMap<CreateUpdatePatientDto, Patient>();
        CreateMap<Store, StoreDto>();
        CreateMap<CreateUpdateStoreDto, Store>();

        CreateMap<RawMaterial, RawMaterialDto>()
            .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Store != null ? s.Store.Name : null));
        CreateMap<CreateUpdateRawMaterialDto, RawMaterial>();
        CreateMap<Session, SessionDto>();
        CreateMap<CreateUpdateSessionDto, Session>();

        CreateMap<SessionMaterial, SessionMaterialDto>();
        CreateMap<CreateUpdateSessionMaterialDto, SessionMaterial>();
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}
