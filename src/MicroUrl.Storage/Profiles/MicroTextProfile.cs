namespace MicroUrl.Storage.Profiles
{
    using AutoMapper;
    using MicroUrl.Storage.Dto;
    using MicroUrl.Storage.Entities;

    public class MicroTextProfile : Profile
    {
        public MicroTextProfile()
        {
            CreateMap<MicroUrlEntity, MicroText>()
                .IncludeBase<MicroUrlEntity, Redirectable>()
                .ForMember(x => x.Language, x => x.MapFrom(p => p.Language))
                .ForMember(x => x.Text, x => x.MapFrom(p => p.Text))
                .ReverseMap();
        }
    }
}