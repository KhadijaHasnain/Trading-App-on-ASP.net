using BIGQXWebsite.Helpers;
using BIGQXWebsite.Models;
using BIGQXWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace BIGQXWebsite.Controllers
{
  [Route("{culture=en}/{Action=Index}")]
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IStringLocalizer _localizer;
    private readonly IResourceContent _resource;

    public HomeController(ILogger<HomeController> logger, IStringLocalizer localizer, IResourceContent resource)
    {
      _logger = logger;
      _localizer = localizer;
      _resource = resource;
    }

    public IActionResult Index()
    {
      ViewData["Title"] = null ;
      ViewData["Description"] = _resource.GetCurrentLanguageByIds(400, 401, 402, 403);
      return View();
    }

    public IActionResult About()
    {
      ViewData["Title"] = _resource.GetCurrentLanguageByIds(3);
      ViewData["Description"] = _resource.GetCurrentLanguageByIds(200, 201);
      return View();
    }

    public IActionResult Contact()
    {
      ViewData["Title"] = _resource.GetCurrentLanguageByIds(6);
      ViewData["Description"] = _resource.GetCurrentLanguageByIds(300, 301);
      return View();
    }

    public IActionResult Partners()
    {
      ViewData["Title"] = _resource.GetCurrentLanguageByIds(4);
      ViewData["Description"] = _resource.GetCurrentLanguageByIds(500, 501);
      return View();
    }

    public IActionResult Services()
    {
      ViewData["Title"] = _resource.GetCurrentLanguageByIds(2);
      ViewData["Description"] = _resource.GetCurrentLanguageByIds(639, 340);
      return View();
    }

    public IActionResult Education()
    {
      ViewData["Title"] = _resource.GetCurrentLanguageByIds(5);
      ViewData["Description"] = _resource.GetCurrentLanguageByIds(400, 401, 402, 403);
      if (LocalizationHelper.GetCode() == "ar")
        return View("Education.ar");
      return View();
    }

    public IActionResult Privacy()
    {
      //ViewData["Title"] = _resource.GetCurrentLanguageByIds(2);
      ViewData["Description"] = _resource.GetCurrentLanguageByIds(639, 340);
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }

}
