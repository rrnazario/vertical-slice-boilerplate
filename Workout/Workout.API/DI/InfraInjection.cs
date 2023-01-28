using FluentValidation;
using MediatR;
using Workout.Infra.Behaviors;
using Workout.Infra.Middlewares;

namespace Workout.API.DI
{
    public static class InfraInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Program).Assembly);
            services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<ErrorCatchingMiddleware>();

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorCatchingMiddleware>();

            return app;
        }
    }

    



}
