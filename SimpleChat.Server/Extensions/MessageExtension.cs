using SimpleChat.Bll.Models;
using SimpleChat.Shared.Contracts;
using SimpleChat.Shared.Contracts.Message;
using System;

namespace SimpleChat.Server.Extensions
{
    /// <summary>
    /// Extensions for message models.
    /// </summary>
    public static class MessageExtensions
    {
        /// <summary>
        /// Converts <see cref="MessageModel"/> model to <seealso cref="MessageContract"/> contract.
        /// </summary>
        /// <param name="model">Message model.</param>
        /// <returns><see cref="MessageContract"/> contract.</returns>
        public static MessageContract ToContract(this MessageModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(MessageModel));

            return new MessageContract
            {
                Id = model.Id,
                Content = model.Content,
                IsRead = model.IsRead,
                CreatedDate = model.CreatedDate,
                UserName = model.UserName,
                Chat = model.Chat?.ToShortInfoContract()
            };
        }

        /// <summary>
        /// Converts <see cref="CreateMessageContract"/> contract to <seealso cref="MessageModel"/> model.
        /// </summary>
        /// <param name="contract">Message contract.</param>
        /// <returns><see cref="MessageModel"/> model.</returns>
        public static MessageModel ToModel(this CreateMessageContract contract)
        {
            if (contract == null)
                throw new ArgumentNullException(nameof(CreateMessageContract));

            return new MessageModel
            {
                Content = contract.Content,
                CreatedDate = DateTime.UtcNow,
                UserName = contract.UserName,
                Chat = new ChatModel()
            };
        }

        /// <summary>
        /// Converts <see cref="EditMessageContract"/> contract to <seealso cref="MessageModel"/> model.
        /// </summary>
        /// <param name="contract">Message contract.</param>
        /// <returns><see cref="MessageModel"/> model.</returns>
        public static MessageModel ToModel(this EditMessageContract contract)
        {
            if (contract == null)
                throw new ArgumentNullException(nameof(EditMessageContract));

            return new MessageModel
            {
                Id = contract.Id,
                Content = contract.Content,
                Chat = new ChatModel { Id = contract.ChatId }
            };
        }
    }
}
