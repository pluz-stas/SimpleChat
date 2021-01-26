using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.Threading.Tasks;
using SimpleChat.Server.Extensions;
using SimpleChat.Shared.Exceptions;

namespace SimpleChat.Server.Filters
{
    /// <summary>
    /// Filter for Http Exceptions.
    /// </summary>
    public class HttpExceptionFilter : Attribute, IAsyncExceptionFilter
    {
        /// <summary>
        /// Called after an action has thrown an System.Exception.
        /// </summary>
        /// <param name="context">The Microsoft.AspNetCore.Mvc.Filters.ExceptionContext.</param>
        /// <returns><see cref="Task"/> A System.Threading.Tasks.Task that on completion indicates the filter has executed.</returns>
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = context.Exception switch
            {
                NotFoundException => (int) HttpStatusCode.NotFound,
                _ => (int) HttpStatusCode.InternalServerError,
            };
            await context.HttpContext.Response.WriteAsJsonAsync(context.Exception.ToContract());
            context.ExceptionHandled = true;
        }
    }
}
