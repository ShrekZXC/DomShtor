using DomShtor.BL.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DomShtor.Middleware;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class SiteAuthorizeAttribute: Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        ICurrentUser? currentUser = context.HttpContext.RequestServices.GetService<ICurrentUser>();
        if (currentUser == null)
        {
            throw new Exception("No user middleware");
        }

        bool isLoggedIn = await currentUser.IsLoggedIn();
        if (isLoggedIn == false)
        {
            context.Result = new RedirectResult("/login");
            return;
        }
    }
}