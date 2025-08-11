using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PracticeMiddleware.Models;

namespace PracticeMiddleware.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var abVersion = HttpContext.Items["ABVersion"]?.ToString();
        var isMobile = HttpContext.Items["IsMobile"] is true;

        ViewBag.Version = abVersion;
        ViewBag.IsMobile = isMobile;
        return View();
    }

    //public IActionResult Index()
    //{
    //    return View();
    //}
    public IActionResult Privacy()
    {
        return Content("? Controller is working. View is not showing.");

       // return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
