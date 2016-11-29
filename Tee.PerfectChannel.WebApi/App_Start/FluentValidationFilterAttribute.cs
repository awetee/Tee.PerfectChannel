using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;

namespace Tee.PerfectChannel.WebApi
{
    public class FluentValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = "There were validation errors",
                    Content = new StringContent(JsonConvert.SerializeObject(new { error = true, actionContext.ModelState }))
                };
            }
        }
    }
}