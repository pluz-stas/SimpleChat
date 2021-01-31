using SimpleChat.Bll.Models;
using SimpleChat.Shared.Contracts.Chat;
using System;
using System.Linq;

namespace SimpleChat.Server.Extensions
{
    /// <summary>
    /// Extensions for chat models.
    /// </summary>
    public static class ChatExtensions
    {
        /// <summary>
        /// Converts <see cref="ChatModel"/> model to <seealso cref="ChatContract"/> contract.
        /// </summary>
        /// <param name="model">Chat model.</param>
        /// <returns><see cref="ChatContract"/> contract.</returns>
        public static ChatContract ToContract(this ChatModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(ChatModel));

            return new ChatContract
            {
                Id = model.Id,
                IsPublic = model.IsPublic,
                Name = model.Name,
                Photo = model.Photo,
                Messages = model.Messages?.Select(x => x?.ToContract())
            };
        }

        /// <summary>
        /// Converts <see cref="ChatModel"/> model to <seealso cref="ShortChatInfoContract"/> contract.
        /// </summary>
        /// <param name="model">Chat model.</param>
        /// <returns><see cref="ShortChatInfoContract"/> contract.</returns>
        public static ShortChatInfoContract ToShortInfoContract(this ChatModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(ChatModel));

            return new ShortChatInfoContract
            {
                Id = model.Id,
                IsPublic = model.IsPublic,
                Name = model.Name,
                Photo = model.Photo,
            };
        }

        /// <summary>
        /// Converts <see cref="CreateChatContract"/> contract to <seealso cref="ChatModel"/> model.
        /// </summary>
        /// <param name="contract">Chat contract.</param>
        /// <returns><see cref="ChatModel"/> model.</returns>
        public static ChatModel ToModel(this CreateChatContract contract)
        {
            if (contract == null)
                throw new ArgumentNullException(nameof(CreateChatContract));

            return new ChatModel
            {
                IsPublic = contract.IsPublic,
                Name = contract.Name,
                Photo = contract.Photo
            };
        }

        /// <summary>
        /// Converts <see cref="EditChatContract"/> contract to <seealso cref="ChatModel"/> model.
        /// </summary>
        /// <param name="contract">Chat contract.</param>
        /// <returns><see cref="ChatModel"/> model.</returns>
        public static ChatModel ToModel(this EditChatContract contract)
        {
            if (contract == null)
                throw new ArgumentNullException(nameof(EditChatContract));

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
