using SimpleChat.Client.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace SimpleChat.Client.Services
{
    public class UserDataStorageService
    {
        private const int UserNameMaxLength = 30;

        private const string DefaultUserName = "anton";
        private const string DefaultUserPic = "images/defaultAvatar.jpg";

        private const string UserNameKey = "UserName";
        private const string UserIdKey = "UserId";
        private const string UserPicKey = "UserImgUrl";

        private readonly ILocalStorageService _localStorageService;

        private string userName;
        private string userPic;
        private string userId;

        public string UserName => userName;

        public string UserPic => userPic;

        public string UserId => userId;

        public UserDataStorageService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task BaseInitializeAsync() => await Task.WhenAll(InitializeUserNameAsync(), InitializeUserPicAsync(), InitializeUserIdAsync());

        public async Task SetUserNameAsync(string userName)
        {
            if (ValidateUserName(userName))
            {
                await _localStorageService.SetStringAsync(UserNameKey, userName);

                this.userName = userName;
            }
        }

        public async Task SetUserPicAsync(string userPic)
        {
            if (ValidateUserPic(userPic))
            {
                await _localStorageService.SetStringAsync(UserPicKey, userPic);

                this.userPic = userPic;
            }
        }

        private async Task InitializeUserIdAsync()
        {
            var userIdFromStorage = await _localStorageService.GetStringAsync(UserIdKey);

            userId = ValidateUserId(userIdFromStorage) ? userIdFromStorage : Guid.NewGuid().ToString();
        }

        private async Task InitializeUserNameAsync()
        {
            var userNameFromStorage = await _localStorageService.GetStringAsync(UserNameKey);

            userName = ValidateUserName(userNameFromStorage) ? userNameFromStorage : DefaultUserName;
        }

        private async Task InitializeUserPicAsync()
        {
            var userPicFromStorage = await _localStorageService.GetStringAsync(UserPicKey);

            userPic = ValidateUserPic(userPicFromStorage) ? userPicFromStorage : DefaultUserPic;
        }

        private static bool ValidateUserName(string name) => !string.IsNullOrWhiteSpace(name) && name.Length <= UserNameMaxLength;
        private static bool ValidateUserPic(string pic) => !string.IsNullOrWhiteSpace(pic);
        private static bool ValidateUserId(string id) => !string.IsNullOrWhiteSpace(id) && Guid.TryParse(id, out var _);
    }
}
