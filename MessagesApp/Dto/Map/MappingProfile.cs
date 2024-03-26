using AutoMapper;
using MessagesApp.Db;

namespace MessagesApp.Dto.Map
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<MessageDto, Message>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Content, o => o.MapFrom(y => y.Content))
                .ForMember(d => d.IsRead, o => o.Ignore())
                .ForMember(d => d.SenderId, o => o.MapFrom(y => y.SenderId))
                .ForMember(d => d.ReceiverId, o => o.MapFrom(y => y.ReceiverId))
                .ReverseMap();
        }
    }
}
