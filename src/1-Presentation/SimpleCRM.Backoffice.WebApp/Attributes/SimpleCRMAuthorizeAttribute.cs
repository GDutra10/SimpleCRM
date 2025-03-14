using Microsoft.AspNetCore.Mvc.Filters;

namespace SimpleCRM.Backoffice.WebApp.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class SimpleCRMAuthorize : Attribute, IFilterFactory, IOrderedFilter
{
    public bool IsReusable => true;
    public int Order => 1;
    
    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredService<ValidateAntiforgeryTokenAuthorizationFilter>();
    }
}