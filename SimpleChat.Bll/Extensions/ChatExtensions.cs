using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using System;

namespace SimpleChat.Bll.Extensions
{
    public static class ChatExtensions
    {
        public static Chat ToEntity (this ChatModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(ChatModel));

            return new Chat()
            {
                Id = model.Id,
                IsPublic = model.IsPublic,
                Name = model.Name,
                Photo = model.Photo,
            };
        }

        public static ChatModel ToModel(this Chat entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(Chat));

            return new ChatModel()
            {
                Id = entity.Id,
                IsPublic = entity.IsPublic,
                Name = entity.Name,
                Photo = entity.Photo,
            };
        }
    }
}
