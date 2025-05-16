using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using estore.Models;
using estore.Utilities;

namespace estore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Logout()
    {
        Functions._userid = 0;
        Functions._username = string.Empty;
        Functions._email = string.Empty;
        Functions._message = string.Empty;
        return RedirectToAction("Index", "Home");
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
