using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Application.Common.Contracts.DTOs;
using SimpleCRM.Backoffice.WebApp.API;

namespace SimpleCRM.Backoffice.WebApp.Controllers;

public class LoginController : BaseController
{
    private readonly ISimpleCRMApi _simpleCRMApi;

    public LoginController(ISimpleCRMApi simpleCRMApi)
    {
        _simpleCRMApi = simpleCRMApi;
    }
    
    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(string email, string password, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View();
        
        var response = await _simpleCRMApi.LoginAsync(new LoginRQ {Email = email, Password = password}, cancellationToken);

        if (!ValidateResponse(response)) 
            return View();
        
        var loginRS = response.Item1!;
        HttpContext.Session.SetString(Constants.Session.AccessToken, loginRS.AccessToken);

        return RedirectToAction("Index", "Home", cancellationToken);
    }

    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        HttpContext.Session.Remove(Constants.Session.AccessToken);
        return RedirectToAction("Index", "Login", cancellationToken);
    }
}