using System;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using SimpleChat.Client.Infrastructure;
using SimpleChat.Client.Resources.Constants;

namespace SimpleChat.Client.Shared
{
    public partial class UserComponent
    {
        private bool isUserNameInputOpen;
        private bool isUserImgInputOpen;
        
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> InputAttributes { get; set; }
        [Inject]
        private ILocalStorageService LocalStorageService { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage = "Too Long")]
        private string UserName { get; set; }
        private string UserImg { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            string localStorageId = await LocalStorageService.GetStringAsync(LocalStorageAttributes.UserIdKeyName);
            if (string.IsNullOrEmpty(localStorageId))
                await LocalStorageService.SetStringAsync(LocalStorageAttributes.UserIdKeyName, Guid.NewGuid().ToString());
            
            string localStorageName = await LocalStorageService.GetStringAsync(LocalStorageAttributes.UserNameKeyName);
            UserName = string.IsNullOrWhiteSpace(localStorageName) ? "anon" : localStorageName;
            UserImg = await LocalStorageService.GetStringAsync(LocalStorageAttributes.UserImgKeyName);
            await base.OnInitializedAsync();
        }
        
        private async Task SetNameAsync()
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                await LocalStorageService.SetStringAsync(LocalStorageAttributes.UserNameKeyName, UserName);
            }
        }       
        
        private async Task SetImgAsync()
        {
            if (!string.IsNullOrWhiteSpace(UserImg))
                await LocalStorageService.SetStringAsync(LocalStorageAttributes.UserImgKeyName, UserImg);
        }
        
        private async Task ToggleEditUserNameInputAsync()
        {
            if (isUserNameInputOpen)
            {
                await SetNameAsync();
            }
            isUserNameInputOpen = !isUserNameInputOpen;
            isUserImgInputOpen = !isUserImgInputOpen && isUserImgInputOpen;
        }

        private async Task ToggleEditUserImgInputAsync()
        {
            if (isUserImgInputOpen)
            {
                await SetImgAsync();
            }
            isUserImgInputOpen = !isUserImgInputOpen;
            isUserNameInputOpen = isUserNameInputOpen && !isUserNameInputOpen;
        }
    }
}
