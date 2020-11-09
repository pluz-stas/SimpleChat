using SimpleChat.Bll.Models;
using SimpleChat.Shared.Contracts;
using System;

namespace SimpleChat.Server.Extensions
{
    /// <summary>
    /// Extensions for chat models.
    /// </summary>
    public static class ChatExtensions
    {
        /// <summary>
        /// Converts <see cref="ChatModel"/> model to <seealso cref="Chat"/> contract.
        /// </summary>
        /// <param name="model">Chat model.</param>
        /// <returns><see cref="Chat"/> contract.</returns>
        public static Chat ToContract(this ChatModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(ChatModel));

            return new Chat
            {
                Id = model.Id,
                IsPublic = model.IsPublic,
                Name = model.Name,
                Photo = model.Photo
            };
        }

        /// <summary>
        /// Converts <see cref="Chat"/> contract to <seealso cref="ChatModel"/> model.
        /// </summary>
        /// <param name="contract">Chat contract.</param>
        /// <returns><see cref="ChatModel"/> model.</returns>
        public static ChatModel ToModel(this Chat contract)
        {
            if (contract == null)
                throw new ArgumentNullException(nameof(Chat));

            return new ChatModel
            {
                Id = contract.Id,
                IsPublic = contract.IsPublic,
                Name = contract.Name,
                Photo = contract.Photo
            };
        }
    }
}
