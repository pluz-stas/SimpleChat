using System;
using System.Linq;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using SimpleChat.Client.Resources.Constants;
using SimpleChat.Shared.Contracts.Message;
using SimpleChat.Shared.Contracts.Chat;
using SimpleChat.Client.Services.Interfaces;
using SimpleChat.Client.Services;

namespace SimpleChat.Client.Pages
{
    public partial class ChatPage
    {
        private string messageInput;
        private ChatContract chat;
        private bool isEditChatModalOpen;


        [Inject]
        private IHttpClientService Http { get; set; }

        [Inject]
        private ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        private ChatTracker ChatTracker { get; set; }

        [Parameter]
        public int ChatId { get; set; }
        
        [Parameter]
        public Guid InviteLink { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if(InviteLink == Guid.Empty)
                chat = await Http.GetAsync<ChatContract>($"api/chats/{ChatId}");
            else
                chat = await Http.GetAsync<ChatContract>($"api/chats/getbylink/{InviteLink}");
            
            ChatTracker.SelectChat(chat.Id);
        }

        private async Task SendAsync()
        {
            var userNameTask = LocalStorageService.GetStringAsync(LocalStorageAttributes.UserNameKeyName);
            var userImgTask = LocalStorageService.GetStringAsync(LocalStorageAttributes.UserImgKeyName);
            var userIdTask = LocalStorageService.GetStringAsync(LocalStorageAttributes.UserIdKeyName);

            await Task.WhenAll(userNameTask, userIdTask, userImgTask);
            
            var user = new ShortUserInfoContract
            {
                UserId = userIdTask.Result,
                UserName = userNameTask.Result,
                UserImg = userImgTask.Result,
            };
                
            var message = new CreateMessageContract
            {
                Content = messageInput,
                User = user,
            };

            await Http.PostAsync($"api/messages/{chat.Id}", message);

            messageInput = string.Empty;
        }
        
        private string GetAvatarName(string str)
        {
            var acronym = new string(str.Split(new [] {' '}, 
                StringSplitOptions.RemoveEmptyEntries).Select(s => s[0]).ToArray()).ToUpper();
            if (acronym.Length > 2)
            {
                acronym = acronym.Substring(0, acronym.Length - (acronym.Length - 2));
            }
            
            return acronym;
        }
    }
}
