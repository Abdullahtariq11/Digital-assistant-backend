using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Digital_assistant_backend.CustomActionFilters
{
    public class ValidateModelAttribute:ActionFilterAttribute 
    {
        
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            
             if(!context.ModelState.IsValid){
               // var errors=context.ModelState.SelectMany( x =>x.Value.Errors.Select(e=>e.ErrorMessage)).ToList();
                //context.Result=new BadRequestObjectResult(errors);

                context.Result=new BadRequestObjectResult(context.ModelState);
                
            }
        }

       

    }
}