using DomShtor.BL;
using DomShtor.BL.Auth;
using DomShtor.Middleware;
using DomShtor.ViewMapper;
using DomShtor.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

namespace DomShtor.Controllers;

[SiteNotAuthorize()]
public class RegisterController : Controller
{
    private readonly IAuth _auth;

    public RegisterController(IAuth auth)
    {
        _auth = auth;
    }

    [HttpGet]
    [Route("/register")]
    public IActionResult Index()
    {
        return View("Index", new RegisterViewModel());
    }

    [HttpPost]
    [Route("/register")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> IndexSave(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _auth.Register(AuthMapper.MapRegisterViewModelToUserModel(model));
                return Redirect("/");
            }
            catch (DuplicateEmailException)
            {
                ModelState.TryAddModelError("Email", "Email уже существует");
            }
            catch (MismatchedPasswordException)
            {
                ModelState.TryAddModelError("password", "Пароли должны совпадать");
            }
        }
        
        return View("Index", model);
    }
}