using System;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using SimpleChat.Client.Infrastructure;
using SimpleChat.Client.Models;

namespace SimpleChat.Client.Shared
{
    public partial class UserComponent
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> InputAttributes { get; set; }
        [Inject] private ILocalStorageService LocalStorageService { get; set; }
        
        private const string UserNameKeyName = "UserName";
        private const string UserImgKeyName = "UserImgUrl";
        
        private bool isUserNameInputOpen;
        private bool isUserImgInputOpen;

        [Required]
        [StringLength(50, ErrorMessage = "Too Long")]
        private string UserName { get; set; }
        private string UserImg { get; set; }

        protected override async Task OnInitializedAsync()
        {
            string localStorageName = await LocalStorageService.GetStringAsync(UserNameKeyName);
            if (localStorageName == null)
            {
                UserName = "anon";
            }
            else
            {
                UserName = localStorageName;
            }
            UserImg = await LocalStorageService.GetStringAsync(UserImgKeyName);
            await base.OnInitializedAsync();
        }
        
        private async Task SetNameAsync()
        {
            if (UserName != null)
            {
                await LocalStorageService.SetStringAsync(UserNameKeyName, UserName);
            }
        }       
        
        private async Task SetImgAsync()
        {
            if (UserImg != null)
            {
                await LocalStorageService.SetStringAsync(UserImgKeyName, UserName);
            }
        }
        
        private async Task ToggleEditUserNameInputAsync()
        {
            if (!isUserImgInputOpen)
            {
                await SetNameAsync();
            }
            isUserNameInputOpen = !isUserNameInputOpen;
            isUserImgInputOpen = !isUserImgInputOpen && isUserImgInputOpen;
        }

        private async Task ToggleEditUserImgInputAsync()
        {
            if (!isUserImgInputOpen)
            {
                await SetImgAsync();
            }
            isUserImgInputOpen = !isUserImgInputOpen;
            isUserNameInputOpen = isUserNameInputOpen && !isUserNameInputOpen;
        }
    }
}
