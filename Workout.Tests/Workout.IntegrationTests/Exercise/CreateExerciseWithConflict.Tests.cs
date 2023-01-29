using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Workout.API.DI;
using static Workout.API.Features.Exercise.CreateExercise;

namespace Workout.IntegrationTests.Exercise
{
    public class CreateExerciseWithConflict
    {
        private readonly ServiceProvider _serviceProvider;
        
        public CreateExerciseWithConflict()
        {
            var _services = new ServiceCollection();
            _services.AddInMemoryDatabase();
            _services.AddInfrastructure();

            _serviceProvider = _services.BuildServiceProvider();
        }

        [Fact]
        public async Task CreatingExerciseWithConflict_Throw()
        {
            var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var command = new CreateExerciseCommand("Burpees", "Description");
            var result = await mediator.Send(command);

            result.Should().BeGreaterThan(0);

            command = new CreateExerciseCommand("Burpees", "Description");
            Action action = () => mediator.Send(command).GetAwaiter().GetResult();

            action.Should().Throw<ValidationException>()
                .And.Message.Should().Contain("Name must be unique");
        }

        [Fact]
        public void CreatingExerciseWithLongName_Throw()
        {
            var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var longName = new string(Enumerable.Range(0, 250).Select(s => 's').ToArray());

            var command = new CreateExerciseCommand(longName!, "Description");
            Action action = () => mediator.Send(command).GetAwaiter().GetResult();

            action.Should().Throw<ValidationException>()
                .And.Message.Should().Contain("Name has exceeded 200 characters");
        }
    }
}
