using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using SimpleChat.Shared.Contracts;
using System;

namespace SimpleChat.Server.Extensions
{
    /// <summary>
    /// Extensions for user models.
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// Converts <see cref="UserModel"/> model to <seealso cref="User"/> contract.
        /// </summary>
        /// <param name="model">User model.</param>
        /// <returns><see cref="User"/> contract.</returns>
        public static User ToContract(this UserModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(UserModel));

            return new User
            {
                Id = model.Id
            };
        }

        /// <summary>
        /// Converts <see cref="User"/> contract to <seealso cref="UserModel"/> model.
        /// </summary>
        /// <param name="contract">User contract.</param>
        /// <returns><see cref="UserModel"/> model.</returns>
        public static UserModel ToModel(this User contract)
        {
            if (contract == null)
                throw new ArgumentNullException(nameof(User));

            return new UserModel
            {
                Id = contract.Id
            };
        }
    }
}
