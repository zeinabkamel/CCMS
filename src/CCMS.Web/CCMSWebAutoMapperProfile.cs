using AutoMapper;
using CCMS.Books;

namespace CCMS.Web;

public class CCMSWebAutoMapperProfile : Profile
{
    public CCMSWebAutoMapperProfile()
    {
        CreateMap<BookDto, CreateUpdateBookDto>();
        
        //Define your object mappings here, for the Web project
    }
}
