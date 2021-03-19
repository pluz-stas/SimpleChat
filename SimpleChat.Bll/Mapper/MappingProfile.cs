using AutoMapper;
using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;

namespace SimpleChat.Bll.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Message, MessageModel>()
                .ReverseMap();

            CreateMap<Chat, ChatModel>()
                .ReverseMap();
        }
    }
}
