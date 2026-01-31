using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Yarito.Endpoint.WebApp.MVC.Filters
{
    public class LogActivityAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var http = context.HttpContext;

            var actionName = context.ActionDescriptor.RouteValues["action"];
            var controllerName = context.ActionDescriptor.RouteValues["controller"];
            var area = context.ActionDescriptor.RouteValues.TryGetValue("area", out var a) ? a : null;
            var username = http.User?.Identity?.Name ?? "Anonymous";
            
            var req = http.Request;

            var method = req.Method;
            var path = req.Path.Value ?? "";
            var ip = http.Connection.RemoteIpAddress?.ToString();
            var userAgent = req.Headers.UserAgent.ToString();
            var traceId = http.TraceIdentifier;
            var statusCode = http.Response?.StatusCode;

            var hasException = context.Exception != null;
            var isInvalidModel = !context.ModelState.IsValid;

            var outcome = hasException ? "Exception"
                : isInvalidModel ? "ValidationFailed"
                : "Success";

            var log = Log
                .ForContext("Area", area)
                .ForContext("Controller", controllerName)
                .ForContext("Action", actionName)
                .ForContext("Outcome", outcome)
                .ForContext("TraceId", traceId)
                .ForContext("StatusCode", statusCode)
                .ForContext("Method", method)
                .ForContext("Path", path)
                .ForContext("IP", ip)
                .ForContext("UserAgent", userAgent)
                .ForContext("Username", username);

            if (hasException)
            {
                log.Error(context.Exception,
                    "{Username} => Request failed in {Controller}/{Action}", username, controllerName, actionName);
            }
            //else if (isInvalidModel)
            //{
            //    var errors = context.ModelState
            //        .Where(kvp => kvp.Value?.Errors.Count > 0)
            //        .Select(kvp => new
            //        {
            //            Field = kvp.Key,
            //            Messages = kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
            //        })
            //        .ToArray();

            //    log.ForContext("ValidationErrors", errors)
            //        .Warning("{Username} => Validation failed in {Controller}/{Action}", username, controllerName, actionName);
            //}
            else
            {
                log.Information("{Username} => Request succeeded in {Controller}/{Action}", username, controllerName, actionName);
            }

            base.OnActionExecuted(context);
        }
    }
}
