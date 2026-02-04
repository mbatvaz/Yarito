using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace Yarito.Endpoint.WebApp.MVC.Middleware
{
    public class ExceptionLoggingMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);

            }
            catch (Exception ex)
            {
                var traceId = context.TraceIdentifier;

                if (context.Response.HasStarted)
                    throw;

                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json; charset=utf-8";

                var payload = new
                {
                    status = "Unexpected error",
                    message = "خطای غیرمنتظره‌ای رخ داد. لطفاً دوباره تلاش کنید.",
                    traceId
                };

                await context.Response.WriteAsJsonAsync(payload);
            }
        }
    }
}
