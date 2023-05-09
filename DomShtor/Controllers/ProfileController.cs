using DomShtor.BL.Auth;
using DomShtor.BL.Profile;
using DomShtor.DAL.Models;
using DomShtor.Middleware;
using DomShtor.Service;
using DomShtor.ViewMapper;
using DomShtor.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DomShtor.Controllers;

[SiteAuthorize()]
public class ProfileController : Controller
{
    private readonly ICurrentUser _currentUser;
    private readonly IProfile _profile;

    public ProfileController(ICurrentUser currentUser,
        IProfile profile)
    {
        _currentUser = currentUser;
        _profile = profile;
    }


    [HttpGet]
    [Route(("/profile"))]
    public async Task<IActionResult> Index()
    {
        var profileModel = await _currentUser.GetProfile();

        return View(profileModel != null
            ? ProfileMapper.MapProfileModelToProfileViewModel(profileModel)
            : new ProfileViewModel());
    }

    [HttpPost]
    [Route(("/profile"))]
    public async Task<IActionResult> IndexSave(ProfileViewModel profileViewModel)
    {
        var userId = await _currentUser.GetCurrentUserId();

        if (userId == null)
            throw new Exception("Пользователь не найден");

        var profile = await _profile.GetByUserId((int)userId);
        if (profileViewModel.ProfileId != null && profileViewModel.ProfileId != profile.ProfileId)
            throw new Exception("Ошибка, разные id пользователей");

        if (ModelState.IsValid)
        {
            var profileModel = ProfileMapper.MapProfileViewModelToProfileModel(profileViewModel);
            profileModel.UserId = (int)userId;
            
            if (Request.Form.Files.Count>0 && Request.Form.Files[0] != null)
            {
                var webFile = new WebFile();
                var fileName = webFile.GetFileName(Request.Form.Files[0].FileName);
                await webFile.UploadAndResizeImage(Request.Form.Files[0].OpenReadStream(), fileName, 800, 600);
                profileModel.ProfileImage = fileName;
            }
            await _profile.AddOrUpdate(profileModel);
            return Redirect("/");
        }

        return View("Index", new ProfileViewModel());
    }
}