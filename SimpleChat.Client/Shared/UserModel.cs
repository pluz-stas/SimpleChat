using Microsoft.AspNetCore.Components;
using SimpleChat.Client.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleChat.Client.Shared
{
    public class UserModel : ComponentBase
    {
        [Inject] public ILocalStorageService LocalStorageService { get; set; }
        internal const string UserStateKeyName = "UserState";
        public UserModel() 
        {
            UserData = new UserViewModel();
        }
        public UserViewModel UserData { get; set; }
        protected async Task SetAsync()
        {
            await LocalStorageService.SetAsync(UserStateKeyName, UserData);
        }
    }
    public class UserViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage ="Too Long")]
        public string Name { get; set; }
        public byte[] Avatar { get; set; }
    }
}
