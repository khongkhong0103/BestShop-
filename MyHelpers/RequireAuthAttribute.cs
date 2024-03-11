using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BestShop.MyHelpers
{
    public class RequireAuthAttribute : Attribute, IPageFilter
    {
        public string RequiredRole { get; set; } = "";
        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            string? role = context.HttpContext.Session.GetString("role");
            if ( role == null )
            {
                // the null is not authenticated => redirect the user to the home page 
                context.Result = new LocalRedirectResult("/");
            }else
            {
                if(RequiredRole.Length > 0 && !RequiredRole.Equals(role) ) {
                   context.Result = new RedirectResult("/");
                }
            }
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
        }
    }
}
