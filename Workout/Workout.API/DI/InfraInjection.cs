using FluentValidation;
using MediatR;
using Workout.Infra.Behaviors;

namespace Workout.API.DI
{
    public static class InfraInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Program).Assembly);
            services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }

      
}
