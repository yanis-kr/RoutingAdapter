using AutoMapper;
using My.Domain.Models.Domain;
using My.Domain.Models.MySys1;
using My.Domain.Models.MySys2;

namespace My.AppCore.Profiles;
public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<DomainAccount, MySys1Account>().ReverseMap();
        CreateMap<DomainAccount, MySys2Account>().ReverseMap();
    }
}
