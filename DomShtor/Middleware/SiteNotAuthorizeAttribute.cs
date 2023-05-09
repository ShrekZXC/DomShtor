using System.Security.Policy;
using DomShtor.BL.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DomShtor.Middleware;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class SiteNotAuthorizeAttribute : Attribute, IAsyncActionFilter
{
    public SiteNotAuthorizeAttribute()
    {
        
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        ICurrentUser? currentUser = context.HttpContext.RequestServices.GetService<ICurrentUser>();

        if (currentUser == null)
        {
            throw new Exception("No user middleware");
        }

        bool isLoggedIn = await currentUser.IsLoggedIn();
        if (isLoggedIn == true)
        {
            context.Result = new RedirectResult("/");
            return;
        }
    }
}