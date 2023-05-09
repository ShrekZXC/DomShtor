using DomShtor.BL.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DomShtor.ViewComponents;

public class MainMenuViewComponent: ViewComponent
{
    private readonly ICurrentUser _currentUser;
    public MainMenuViewComponent(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        bool isLoggedIn = await _currentUser.IsLoggedIn();
        return View("Index", isLoggedIn);
    }
    
}