using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using System;

namespace SimpleChat.Bll.Extensions
{
    public static class MessageExtensions
    {
        public static Message ToEntity(this MessageModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(MessageModel));

            return new Message()
            {
                Id = model.Id,
                ChatId = model.ChatId,
                UserId = model.UserId,
                Content = model.Content,
                CreatedDate = model.CreatedDate,
                IsRead = model.IsRead,
                User = model.User?.ToEntity()
            };
        }

        public static MessageModel ToModel(this Message entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(Message));

            return new MessageModel()
            {
                Id = entity.Id,
                ChatId = entity.ChatId,
                UserId = entity.UserId,
                Content = entity.Content,
                CreatedDate = entity.CreatedDate,
                IsRead = entity.IsRead,
                User = entity.User?.ToModel()
            };
        }
    }
}
