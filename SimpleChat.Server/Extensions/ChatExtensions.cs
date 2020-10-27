using SimpleChat.Bll.Models;
using SimpleChat.Shared.Contracts;
using System;

namespace SimpleChat.Server.Extensions
{
    public static class ChatExtensions
    {
        public static Chat ToContract(this ChatModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(ChatModel));

            return new Chat
            {
                Id = model.Id
            };
        }

        public static ChatModel ToModel(this Chat contract)
        {
            if (contract == null)
                throw new ArgumentNullException(nameof(Chat));

            return new ChatModel
            {
                Id = contract.Id
            };
        }
    }
}
