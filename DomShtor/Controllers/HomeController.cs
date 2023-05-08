using System.Diagnostics;
using DomShtor.BL.Auth;
using Microsoft.AspNetCore.Mvc;
using DomShtor.Models;

namespace DomShtor.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICurrentUser _currentUser;

    public HomeController(ILogger<HomeController> logger, ICurrentUser currentUser)
    {
        _logger = logger;
        _currentUser = currentUser;
    }

    public async Task<IActionResult> Index()
    {
        var isLoggedIn = await _currentUser.IsLoggedIn();
        return View(isLoggedIn);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}