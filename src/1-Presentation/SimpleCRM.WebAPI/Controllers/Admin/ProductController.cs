using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Application.Admin.Contracts.DTOs;
using SimpleCRM.Application.Admin.Contracts.Services;
using SimpleCRM.Application.Attendant.Contracts.DTOs;
using SimpleCRM.Application.Common.Contracts.DTOs;

namespace SimpleCRM.WebAPI.Controllers.Admin;

[Authorize]
[ApiController]
[Route("Admin/[controller]s")]
public class ProductController : AppBaseController
{
    private readonly ILogger<ProductController> _logger;
    private readonly IAdminProductService _adminProductService;
    
    public ProductController(ILogger<ProductController> logger, IAdminProductService adminProductService)
    {
        _logger = logger;
        _adminProductService = adminProductService;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ProductRS), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationRS), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorRS), (int)HttpStatusCode.InternalServerError)]
    public async Task<ProductRS> ProductRegisterAsync(ProductRegisterRQ productRegisterRQ, CancellationToken cancellationToken)
    {
        return await _adminProductService.RegisterProductAsync(productRegisterRQ, cancellationToken);
    }

    [HttpPut]
    [Authorize]
    [ProducesResponseType(typeof(ProductRS), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationRS), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorRS), (int)HttpStatusCode.InternalServerError)]
    public async Task<ProductRS> ProductUpdateAsync(ProductUpdateRQ productUpdateRQ, CancellationToken cancellationToken)
    {
        return await _adminProductService.UpdateProductAsync(productUpdateRQ, cancellationToken);
    }
}