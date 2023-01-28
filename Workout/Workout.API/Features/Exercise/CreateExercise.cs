using MediatR;
using Workout.API.SeedWork;
using Workout.Infra.Persistence;

namespace Workout.API.Features.Exercise
{
    public class CreateExercise
    {
        public record CreateExerciseCommand(
            string Name,
            string Description
            ) : IRequest<int>;

        public record CreatedExerciseResponse(
            int Id);

        public class Handler
            : RequestHandlerBase, IRequestHandler<CreateExerciseCommand, int>
        {
            public Handler(WorkoutContext workoutContext) : base(workoutContext)
            {
            }

            public async Task<int> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
            {
                var result = await _workoutContext.Exercises.AddAsync(new Domain.Model.Exercise(request.Name, request.Description, string.Empty));
                await _workoutContext.SaveChangesAsync();

                return result.Entity.Id;
            }
        }
    }
}
