namespace MicroUrl.Storage.Profiles
{
    using AutoMapper;
    using MicroUrl.Storage.Dto;
    using MicroUrl.Storage.Entities;

    public class MicroUrlProfile : Profile
    {
        public MicroUrlProfile()
        {
            CreateMap<MicroUrlEntity, MicroUrl>()
                .IncludeBase<MicroUrlEntity, Redirectable>()
                .ForMember(x => x.Url, x => x.MapFrom(p => p.Url));
        }
    }
}