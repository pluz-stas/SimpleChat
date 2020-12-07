using SimpleChat.Bll.Models;
using SimpleChat.Shared.Contracts;
using System;

namespace SimpleChat.Server.Extensions
{
    /// <summary>
    /// Extensions for message models.
    /// </summary>
    public static class MessageExtensions
    {
        /// <summary>
        /// Converts <see cref="MessageModel"/> model to <seealso cref="Message"/> contract.
        /// </summary>
        /// <param name="model">Message model.</param>
        /// <returns><see cref="Message"/> contract.</returns>
        public static Message ToContract(this MessageModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(MessageModel));

            return new Message
            {
                Id = model.Id,
                Content = model.Content,
                IsRead = model.IsRead,
                CreatedDate = model.CreatedDate,
                UserName = model.UserName
            };
        }

        /// <summary>
        /// Converts <see cref="Message"/> contract to <seealso cref="MessageModel"/> model.
        /// </summary>
        /// <param name="contract">Message contract.</param>
        /// <returns><see cref="MessageModel"/> model.</returns>
        public static MessageModel ToModel(this Message contract)
        {
            if (contract == null)
                throw new ArgumentNullException(nameof(Message));

            return new MessageModel
            {
                Id = contract.Id,
                Content = contract.Content,
                IsRead = contract.IsRead,
                CreatedDate = contract.CreatedDate.HasValue ? contract.CreatedDate.Value.ToUniversalTime() : DateTime.UtcNow,
                UserName = contract.UserName
            };
        }
    }
}
