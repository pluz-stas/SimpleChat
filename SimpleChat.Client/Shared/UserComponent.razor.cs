using System;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using SimpleChat.Client.Infrastructure;
using static System.String;

namespace SimpleChat.Client.Shared
{
    public partial class UserComponent
    {
        private const string UserNameKeyName = "UserName";
        private const string UserImgKeyName = "UserImgUrl";
        private const string UserIdKeyName = "UserId";
        
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
            string localStorageId = await LocalStorageService.GetStringAsync(UserIdKeyName);
            if (IsNullOrEmpty(localStorageId))
                await LocalStorageService.SetStringAsync(UserIdKeyName, Guid.NewGuid().ToString());
            
            string localStorageName = await LocalStorageService.GetStringAsync(UserNameKeyName);
            UserName = IsNullOrWhiteSpace(localStorageName) ? "anon" : localStorageName;
            UserImg = await LocalStorageService.GetStringAsync(UserImgKeyName);
            await base.OnInitializedAsync();
        }
        
        private async Task SetNameAsync()
        {
            if (!IsNullOrWhiteSpace(UserName))
            {
                await LocalStorageService.SetStringAsync(UserNameKeyName, UserName);
            }
        }       
        
        private async Task SetImgAsync()
        {
            if (!IsNullOrWhiteSpace(UserImg))
                await LocalStorageService.SetStringAsync(UserImgKeyName, UserImg);
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
