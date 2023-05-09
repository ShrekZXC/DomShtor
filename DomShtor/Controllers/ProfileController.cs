using System.Security.Cryptography;
using DomShtor.Middleware;
using DomShtor.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;

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
            string fileName = "";
            var imageData = Request.Form.Files[0];
            if (imageData != null)
            {
                MD5 md5hash = MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(imageData.FileName);
                byte[] hashBytes = md5hash.ComputeHash(inputBytes);

                string hash = Convert.ToHexString(hashBytes);

                var dir = "./wwwroot/images/" + hash.Substring(0, 2) + "/" +
                          hash.Substring(0, 4);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                fileName = dir + "/" + imageData.FileName;

                using (var stream = System.IO.File.Create(fileName))
                    await imageData.CopyToAsync(stream);
                }
        // }

        return View("Index", new ProfileViewModel());
    }
}