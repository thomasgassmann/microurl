namespace MicroUrl.Storage.Profiles
{
    using AutoMapper;
    using MicroUrl.Storage.Dto;
    using MicroUrl.Storage.Entities;

    public class VisitProfile : Profile
    {
        public VisitProfile()
        {
            CreateMap<VisitEntity, Visit>()
                .ForMember(x => x.Created, x => x.MapFrom(p => p.Created))
                .ForMember(x => x.Headers, x => x.MapFrom(p => p.Headers))
                .ForMember(x => x.Id, x => x.MapFrom(p => p.Id))
                .ForMember(x => x.Key, x => x.MapFrom(p => p.Key))
                .ReverseMap();
        }
    }
}