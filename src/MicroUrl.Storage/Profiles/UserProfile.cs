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
                .ForMember(x => x.Password, x => x.MapFrom(p => new UserPassword
                {
                    Hash = p.PasswordHash,
                    Iterations = p.PasswordIterations,
                    Salt = p.PasswordSalt
                }));

            CreateMap<User, UserEntity>()
                .ForMember(x => x.PasswordHash, x => x.MapFrom(p => p.Password.Hash))
                .ForMember(x => x.PasswordSalt, x => x.MapFrom(p => p.Password.Salt))
                .ForMember(x => x.PasswordIterations, x => x.MapFrom(p => p.Password.Iterations))
                .ForMember(x => x.Created, x => x.MapFrom(p => p.Created))
                .ForMember(x => x.UserName, x => x.MapFrom(p => p.UserName));
        }
    }
}
