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
    public async Task<IActionResult> Index(string email, string password, CancellationToken cancellationToken)
    {
        var response = await _simpleCRMApi.LoginAsync(new LoginRQ {Email = email, Password = password}, cancellationToken);

        if (!ValidateResponse(response)) 
            return RedirectToAction("Index", "Home");

        return View();
    }
}