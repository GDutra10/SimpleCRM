using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Application.Attendant.Contracts;
using SimpleCRM.Application.Attendant.Contracts.DTOs;

namespace SimpleCRM.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]s")]
public class CustomerController : AppBaseController
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerService _customerService;
    
    public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CustomerRS), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationRS), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorRS), (int)HttpStatusCode.InternalServerError)]
    public async Task<CustomerRS> CustomerRegisterAsync(CustomerRegisterRQ customerRegisterRQ, CancellationToken cancellationToken)
    {
        var accessToken = this.GetAccessTokenFromHeader();
        
        return await _customerService.RegisterCustomerAsync(accessToken, customerRegisterRQ, cancellationToken);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(CustomerSearchRS), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationRS), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorRS), (int)HttpStatusCode.InternalServerError)]
    public async Task<CustomerSearchRS> CustomerSearchAsync([FromQuery]CustomerSearchRQ customerSearchRQ, CancellationToken cancellationToken)
    {
        return await _customerService.SearchCustomerAsync(customerSearchRQ, cancellationToken);
    }
    
}