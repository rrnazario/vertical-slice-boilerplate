using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Workout.Infra.Middlewares
{
    public class ErrorCatchingMiddleware
        : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                object content = Debugger.IsAttached
                    ? new
                    {
                        e.Message,
                        e.StackTrace
                    }
                    : new
                    {
                        e.Message
                    };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(content));
            }
        }
    }
}
