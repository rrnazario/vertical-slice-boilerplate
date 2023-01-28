using Light.GuardClauses;
using MediatR;
using Workout.API.SeedWork;
using Workout.Domain.SeedWork;
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
            public UpdateRepetitionsCommandHandler(IUnitOfWork workoutContext)
                : base(workoutContext) { }

            public async Task<Unit> Handle(UpdateRepetitionsCommand request, CancellationToken cancellationToken)
            {
                var serie = await _unitOfWork.Series.FindAsync(request.Id);
                serie.MustNotBeNull();

                serie!.UpdateRepetitions(request.Amount);
                _unitOfWork.Update(serie);

                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
