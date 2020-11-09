using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using System;

namespace SimpleChat.Bll.Extensions
{
    public static class UserExtensions
    {
        public static User ToEntity(this UserModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(UserModel));

            return new User()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static UserModel ToModel(this User entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(User));

            return new UserModel()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
