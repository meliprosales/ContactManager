using AutoMapper;
using ContactManager.BusinessLogic;

namespace ContactManager.Web.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contact, ContactViewModel>()
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(dest => dest.FormattedAddress, opt => opt.MapFrom(src => src.Address.Street + ", " + src.Address.City + ", " + src.Address.State + " " + src.Address.PostalCode + ", United States"))
                .ReverseMap();
        }
    }
}
