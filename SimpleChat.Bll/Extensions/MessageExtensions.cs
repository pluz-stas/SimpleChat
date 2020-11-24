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
                Content = model.Content,
                CreatedDate = model.CreatedDate,
                IsRead = model.IsRead,
            };
        }

        public static MessageModel ToModel(this Message entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(Message));

            return new MessageModel()
            {
                Id = entity.Id,
                Content = entity.Content,
                CreatedDate = entity.CreatedDate,
                IsRead = entity.IsRead,
            };
        }
    }
}
