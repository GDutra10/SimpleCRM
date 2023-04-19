using Microsoft.AspNetCore.Mvc.Filters;
using SimpleCRM.Application.Common.Constants;
using SimpleCRM.Application.Common.Contracts.DTOs;
using SimpleCRM.Domain.Common.System.Exceptions;

namespace SimpleCRM.WebAPI.ActionFilters;

public class SearchFilterActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var param = context.ActionArguments.SingleOrDefault(p => p.Value is BaseSearchRQ);

        if (param.Value == null) 
            return;
        
        var getBaseListRQ = ((BaseSearchRQ) param.Value); 
        
        if (getBaseListRQ.PageSize > SearchConstants.PageSizeDefault)
            throw new BusinessException(nameof(getBaseListRQ.PageSize), "Page Size must be equals or less than 50");

        if (getBaseListRQ.PageSize <= 0)
            getBaseListRQ.PageSize = SearchConstants.PageSizeDefault;

        if (getBaseListRQ.PageNumber < 0)
            getBaseListRQ.PageNumber = SearchConstants.PageNumberDefault;
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}