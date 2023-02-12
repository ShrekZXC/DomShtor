using DomShtor.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DomShtor.Controllers;

public class AccountController: Controller
{
    [HttpGet]
    [AllowAnonymous]
    [Route("/forgotPassword")]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            
        }
    }
}