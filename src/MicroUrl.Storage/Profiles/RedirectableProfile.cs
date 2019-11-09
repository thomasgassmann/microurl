namespace MicroUrl.Storage.Profiles
{
    using AutoMapper;
    using MicroUrl.Storage.Dto;
    using MicroUrl.Storage.Entities;

    public class RedirectableProfile : Profile
    {
        public RedirectableProfile()
        {
            CreateMap<MicroUrlEntity, Redirectable>()
                .ForMember(x => x.Created, x => x.MapFrom(p => p.Created))
                .ForMember(x => x.Enabled, x => x.MapFrom(p => p.Enabled))
                .ForMember(x => x.Key, x => x.MapFrom(p => p.Key));
        }
    }
}