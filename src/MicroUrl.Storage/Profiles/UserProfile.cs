namespace MicroUrl.Storage.Profiles
{
    using AutoMapper;
    using MicroUrl.Storage.Dto;
    using MicroUrl.Storage.Entities;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, User>()
                .ForMember(x => x.Created, x => x.MapFrom(p => p.Created))
                .ForMember(x => x.UserName, x => x.MapFrom(p => p.UserName))
                .ForMember(x => x.PasswordHash, x => x.MapFrom(p => p.PasswordHash))
                .ReverseMap();
        }
    }
}
