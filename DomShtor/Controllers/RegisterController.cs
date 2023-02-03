using DomShtor.BL.Auth;
using DomShtor.ViewMapper;
using DomShtor.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DomShtor.Controllers;

public class RegisterController : Controller
{
    private readonly IAuthBL _authBL;

    public RegisterController(IAuthBL authBl)
    {
        _authBL = authBl;
    }

    [HttpGet]
    [Route("/register")]
    public IActionResult Index()
    {
        return View("Index", new RegisterViewModel());
    }
    
    [HttpPost]
    [Route("/register")]
    public async Task<IActionResult> IndexSave(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Password == model.ReenterPassword)
            {
                await _authBL.CreateUser(AuthMapper.MapRegisterViewModelToUserModel(model));
                return Redirect("/");
            }
        }
        return View("Index", model);
    }
}