using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Application.Backoffice.Contracts.DTOs;
using SimpleCRM.Backoffice.WebApp.API;
using SimpleCRM.Backoffice.WebApp.Attributes;
using SimpleCRM.Domain.Common.Enums;

namespace SimpleCRM.Backoffice.WebApp.Controllers;

public class OrderController : BaseController
{
    private readonly ISimpleCRMApi _simpleCRMApi;

    public OrderController(ISimpleCRMApi simpleCRMApi)
    {
        _simpleCRMApi = simpleCRMApi;
    } 
    
    [SimpleCRMAuthorize]
    public async Task<IActionResult> Index(string orderId, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(orderId))
        {
            var orderResponse = await _simpleCRMApi.GetOrderAsync(GetAccessToken(), orderId, cancellationToken);
            ViewBag.OrderRS = orderResponse.Item1!; 
            ViewBag.OrderId = orderId;
            
            if (ValidateResponse(orderResponse))
            {
                var interactionResponse = await _simpleCRMApi.StartInteractionAsync(GetAccessToken(), orderId, cancellationToken);

                if (ValidateResponse(orderResponse) && interactionResponse.Item1 is not null)
                {
                    ViewBag.InteractionStartRS = interactionResponse.Item1;
                    
                    return View();    
                }
            }
                
        }
        
        var orderSearchRQ = new OrderSearchRQ() {OrderState = OrderState.PreConfirmed};
        var response =  await _simpleCRMApi.GetOrdersAsync(GetAccessToken(), orderSearchRQ, cancellationToken);
        ValidateResponse(response);
        ViewBag.OrderSearchRS = response.Item1!;

        return View(); 
    }
}