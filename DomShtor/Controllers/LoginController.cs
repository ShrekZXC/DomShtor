using DomShtor.BL.Auth;
using DomShtor.Middleware;
using DomShtor.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DomShtor.Controllers;

[SiteNotAuthorize()]
public class LoginController: Controller
{
    private readonly IAuth _auth;
    public LoginController(IAuth auth)
    {
        _auth = auth;
    }
    
    [HttpGet]
    [Route("/login")]
    public IActionResult Index()
    {
        return View("Index", new LoginViewModel());
    }
    
    [HttpPost]
    [Route("/login")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> IndexSave(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _auth.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
                return Redirect("/");
            }
            catch (DomShtor.BL.AuthorizationException e)
            {
                ModelState.AddModelError("Email", "Имя или Email неверные");
            }
        }
        return View("Index", model);
    }
    
}