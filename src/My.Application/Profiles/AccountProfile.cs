using AutoMapper;
using My.Domain.Models.Domain;
using My.Domain.Models.Legacy;
using My.Domain.Models.Modern;

namespace My.Application.Profiles;
public class AccountProfile : Profile
{
    public AccountProfile()
    {
        //CreateMap<DomainAccount, LegacyAccount>().ReverseMap()
        //.ForMember(dest => dest.DomainField, opt => opt.MapFrom(src => src.LegacyField));
        CreateMap<DomainAccount, LegacyAccount>()
            .ForMember(dest => dest.LegacyField, opt => opt.MapFrom(src => src.DomainField))
            .ReverseMap();
        //.ForMember(dest => dest.DomainField, opt => opt.MapFrom(src => src.LegacyField));

        CreateMap<DomainAccount, ModernAccount>()
            .ForMember(dest => dest.ModernField, opt => opt.MapFrom(src => src.DomainField))
            .ReverseMap();

        CreateMap<DomainAccount, DomainAccountResponse>().ReverseMap();
    }
}
