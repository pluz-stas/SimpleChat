using Microsoft.AspNetCore.Http;
using SimpleChat.Bll.Interfaces;
using System.Linq;
using System.Threading.Tasks;
 
public class SessionMiddleware
{
    private readonly RequestDelegate _next;

    public SessionMiddleware(RequestDelegate next)
    {
        this._next = next;
    }
 
    public async Task InvokeAsync(HttpContext context, IUserService service)
    {
        if (context.Session.Keys.Contains("auth"))
        {
            context.User = await service.GetBySissionKeyAsync(context.Session.GetString("auth"));
            await _next.Invoke(context);
        }
        else
        {
            //context.Session.SetString("auth", "tom");
            context.Response.Redirect("/login");
            await _next.Invoke(context);
        }
    }
}