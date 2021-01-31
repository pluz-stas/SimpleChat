using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using SimpleChat.Shared.Exceptions;

namespace SimpleChat.Server.Filters
{
    /// <summary>
    /// Filter for Http Exceptions.
    /// </summary>
    public class HttpExceptionFilter : Attribute, IAsyncExceptionFilter
    {
        /// <summary>
        /// Called after an action has thrown an Exception.
        /// </summary>
        /// <param name="context">The Microsoft.AspNetCore.Mvc.Filters.ExceptionContext.</param>
        /// <returns><see cref="Task"/> A System.Threading.Tasks.Task that on completion indicates the filter has executed.</returns>
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = context.Exception switch
            {
                NullReferenceException => StatusCodes.Status400BadRequest,
                ArgumentException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError,
            };

            await context.HttpContext.Response.WriteAsJsonAsync(context.Exception.Message);
            context.ExceptionHandled = true;
        }
    }
}
