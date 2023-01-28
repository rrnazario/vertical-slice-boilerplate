using Light.GuardClauses;
using MediatR;
using Workout.API.SeedWork;
using Workout.Infra.Persistence;

namespace Workout.API.Features.Serie
{
    public class UpdateSerieRepetitions
    {
        public record UpdateRepetitionsCommand(
            int Id,
            int Amount
            ) : IRequest;

        public class UpdateRepetitionsCommandHandler
            : RequestHandlerBase, IRequestHandler<UpdateRepetitionsCommand>

        {
            public UpdateRepetitionsCommandHandler(WorkoutContext workoutContext) : base(workoutContext)
            {
            }

            public async Task<Unit> Handle(UpdateRepetitionsCommand request, CancellationToken cancellationToken)
            {
                var serie = await _workoutContext.Series.FindAsync(request.Id);
                serie.MustNotBeNull();

                serie!.UpdateRepetitions(request.Amount);
                _workoutContext.Update(serie);

                await _workoutContext.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
