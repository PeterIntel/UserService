using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace User.Service.CustomFilters
{
    public class CustomJSONExceptionFilter : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            if(context.HttpContext.Request.GetTypedHeaders().Accept.Any(header => header.MediaType == "application/json" || header.MediaType == "*/*"))
            {
                var jsonResult = new JsonResult(new { error = context.Exception.Message });
                jsonResult.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                context.Result = jsonResult;
            }

            await base.OnExceptionAsync(context);
        }
    }
}
