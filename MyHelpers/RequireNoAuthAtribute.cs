﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BestShop.MyHelpers
{
    public class RequireNoAuthAtribute : Attribute, IPageFilter
    {
        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            string? role = context.HttpContext.Session.GetString("role");
            if (role != null)
            {
                // the user is already auhenticated => redirect the user to the home page
                context.Result = new RedirectResult("/");
            }
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
        }
    }
}
