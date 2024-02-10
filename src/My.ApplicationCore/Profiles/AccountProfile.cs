using AutoMapper;
using My.Domain.Models.Domain;
using My.Domain.Models.MySys1;
using My.Domain.Models.MySys2;

namespace My.AppCore.Profiles;
public class AccountProfile : Profile
{
    public AccountProfile()
    {
        //CreateMap<DomainAccount, MySys1Account>().ReverseMap()
        //.ForMember(dest => dest.DomainField, opt => opt.MapFrom(src => src.MySys1Field));
        CreateMap<DomainAccount, MySys1Account>()
            .ForMember(dest => dest.MySys1Field, opt => opt.MapFrom(src => src.DomainField))
            .ReverseMap();
            //.ForMember(dest => dest.DomainField, opt => opt.MapFrom(src => src.MySys1Field));

        CreateMap<DomainAccount, MySys2Account>()
            .ForMember(dest => dest.MySys2Field, opt => opt.MapFrom(src => src.DomainField))
            .ReverseMap();

        CreateMap<DomainAccount, DomainAccountResponse>().ReverseMap();
    }
}
