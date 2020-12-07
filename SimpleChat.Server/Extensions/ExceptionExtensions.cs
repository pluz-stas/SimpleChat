using SimpleChat.Bll.Models;
using SimpleChat.Shared.Contracts;
using System;

namespace SimpleChat.Server.Extensions
{
    /// <summary>
    /// Extensions for message models.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Converts <see cref="Exception"/> model to <seealso cref="ExceptionContract"/> contract.
        /// </summary>
        /// <param name="model">ExceptionContract model.</param>
        /// <returns><see cref="ExceptionContract"/> contract.</returns>
        public static ExceptionContract ToContract(this Exception model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(Exception));

            return new ExceptionContract
            {
                Title = model.GetType().Name,
                Message = model.Message,
            };
        }
    }
}
