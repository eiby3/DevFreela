﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DevFreela.API.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {            
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //if (!context.ModelState.IsValid)
            //{
            //    var messages = context.ModelState
            //    .SelectMany(x => x.Value.Errors)
            //    .Select(x => x.ErrorMessage)
            //    .ToList();

            //    context.Result = new BadRequestObjectResult(messages);
            //}
        }
    }
}
