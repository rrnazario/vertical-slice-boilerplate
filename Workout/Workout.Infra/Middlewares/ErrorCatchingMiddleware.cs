using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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
                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(new
                    {
                        error = e.Message
                    }));
            }
        }
    }
}
