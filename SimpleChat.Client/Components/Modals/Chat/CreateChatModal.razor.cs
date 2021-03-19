﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SimpleChat.Client.Resources.Constants;
using SimpleChat.Client.Resources.ResourceFiles;
using SimpleChat.Client.Services;
using SimpleChat.Shared.Contracts.Chat;

namespace SimpleChat.Client.Components.Modals.Chat
{
    public partial class CreateChatModal
    {
        private bool isPublic;
        private string chatName;
        private byte[] photo;

        [Inject]
        private IHttpClientService Http { get; set; }

        [Parameter] 
        public EventCallback OnClose { get; set; }
        
        [Inject]
        private ErrorStateService ErrorState { get; set; }
        
        [Inject]
        private LoadFileService LoadFileService { get; set; }


        private async Task CreateAsync()
        {
            var chat = new CreateChatContract
            {
                IsPublic = isPublic,
                Name = chatName,
                Photo = photo,
            };

            await Http.PostAsync($"api/chats/", chat);
            OnClose.InvokeAsync();
        }

        private async Task LoadPhoto(InputFileChangeEventArgs e) => photo = await LoadFileService.LoadFile(e);
    }
}