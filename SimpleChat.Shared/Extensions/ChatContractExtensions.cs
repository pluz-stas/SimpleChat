using SimpleChat.Shared.Contracts.Chat;
using System;

namespace SimpleChat.Shared.Extensions
{
    /// <summary>
    /// Extensions for chat models.
    /// </summary>
    public static class ChatContractExtensions
    {
        /// <summary>
        /// Converts <see cref="ChatContract"/> model to <seealso cref="EditChatContract"/> contract.
        /// </summary>
        /// <param name="model">Chat contract.</param>
        /// <returns><see cref="EditChatContract"/> contract.</returns>
        public static EditChatContract ToEditContract(this ChatContract model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(ChatContract));

            return new EditChatContract
            {
                Id = model.Id,
                IsPublic = model.IsPublic,
                Name = model.Name,
                Photo = model.Photo,
                IsMasterPassword = model.IsMasterPassword,
            };
        }
    }
}