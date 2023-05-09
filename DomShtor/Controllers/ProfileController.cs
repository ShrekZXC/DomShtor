using DomShtor.Middleware;
using DomShtor.Service;
using DomShtor.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DomShtor.Controllers;

[SiteAuthorize()]
public class ProfileController : Controller
{
    [HttpGet]
    [Route(("/profile"))]
    public IActionResult Index()
    {
        return View(new ProfileViewModel());
    }

    [HttpPost]
    [Route(("/profile"))]
    public async Task<IActionResult> IndexSave()
    {
        // if (ModelState.IsValid())
        // {
        var imageData = Request.Form.Files[0];
            if (imageData != null)
            {
                var webFile = new WebFile();
                var fileName = webFile.GetFileName(imageData.FileName);
                await webFile.UploadAndResizeImage(imageData.OpenReadStream(), fileName, 800, 600);
            }
        // }

        return View("Index", new ProfileViewModel());
    }
}