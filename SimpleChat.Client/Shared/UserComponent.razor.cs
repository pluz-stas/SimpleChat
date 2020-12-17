using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
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
        private const string UserStateKeyName = "UserState";
        public UserComponent()
        {
            UserData = new UserModel();
        }
        
        private UserModel UserData { get; set; }
        private async Task SetAsync()
        {
            await LocalStorageService.SetAsync(UserStateKeyName, UserData);
        }
    }
}
