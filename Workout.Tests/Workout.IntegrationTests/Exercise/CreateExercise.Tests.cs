using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Workout.API.DI;
using static Workout.API.Features.Exercise.CreateExercise;

namespace Workout.IntegrationTests.Exercise
{
    public class CreateExercise
    {
        private readonly ServiceProvider _serviceProvider;
        
        public CreateExercise()
        {
            var _services = new ServiceCollection();
            _services.AddInMemoryDatabase();
            _services.AddInfrastructure();

            _serviceProvider = _services.BuildServiceProvider();
        }
        
        [Fact]
        public async Task CreatingExerciseWithoutConflict()
        {
            var scope = _serviceProvider.CreateScope();
            var mediator =scope.ServiceProvider.GetRequiredService<IMediator>();

            var command = new CreateExerciseCommand("Burpees", "Description");
            var result = await mediator.Send(command);

            result.Should().BeGreaterThan(0);
        }       
    }
}
