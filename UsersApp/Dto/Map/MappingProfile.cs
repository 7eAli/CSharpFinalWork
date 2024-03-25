using AutoMapper;
using UsersApp.Db;

namespace UsersApp.Dto.Map
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Email, o => o.MapFrom(y => y.Email))
                .ForMember(d => d.Password, o => o.MapFrom(y => y.Password))
                .ForMember(d => d.RoleId, o => o.MapFrom(y => y.RoleId))                
                .ForMember(d => d.Role, o => o.Ignore())
                .ReverseMap();
        }
    }
}
