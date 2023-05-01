using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Application.Attendant.Contracts.DTOs;
using SimpleCRM.Application.Common.Contracts.DTOs;
using SimpleCRM.Application.Common.Contracts.Services;
using SimpleCRM.WebAPI.ActionFilters;

namespace SimpleCRM.WebAPI.Controllers.Common;

[Authorize]
[ApiController]
[Route("Common/[controller]s")]
public class ProductController : AppBaseController
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductBaseService _productBaseService;
    
    public ProductController(ILogger<ProductController> logger, IProductBaseService productBaseService)
    {
        _logger = logger;
        _productBaseService = productBaseService;
    }
    
    [HttpGet]
    [Authorize]
    [ServiceFilter(typeof(SearchFilterActionFilter), Order = 1)]
    [ProducesResponseType(typeof(CustomerSearchRS), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationRS), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorRS), (int)HttpStatusCode.InternalServerError)]
    public async Task<ProductSearchRS> CustomerSearchAsync([FromQuery]ProductSearchRQ productSearchRQ, CancellationToken cancellationToken)
    {
        return await _productBaseService.SearchProductsAsync(productSearchRQ, cancellationToken);
    } 
}