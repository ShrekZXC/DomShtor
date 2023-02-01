using DomShtor.BL.Auth;
using DomShtor.ViewMapper;
using DomShtor.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DomShtor.Controllers;

public class LoginController: Controller
{
    private readonly IAuthBL _authBL;

    public LoginController(IAuthBL authBl)
    {
        _authBL = authBl;
    }
    
    [HttpGet]
    [Route("/login")]
    public IActionResult Index()
    {
        return View("Index", new LoginViewModel());
    }
    
    [HttpPost]
    [Route("/login")]
    public async Task<IActionResult> IndexSave(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _authBL.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
            return Redirect("/");
        }
        return View("Index", model);
    }
    
}