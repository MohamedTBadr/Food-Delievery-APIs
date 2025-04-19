using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commerce.Factories
{
    public static class APIResponseFactory
    {
        public static IActionResult GenerateAPIResponse(ActionContext context)
        {
           
                //get model the entries in model state that has validation errors
                var errors = context.ModelState.Where(m => m.Value.Errors.Any()).Select(m => new ValidationsError
                {
                    Field = m.Key,
                    Errors = m.Value.Errors.Select(m => m.ErrorMessage)
                });
                var response = new ValidationsErrorResponse { Errors = errors };
                return new BadRequestObjectResult(response);
            
        }
    }
}
