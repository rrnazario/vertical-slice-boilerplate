using FluentValidation;
using MediatR;
using Workout.API.SeedWork;
using Workout.Domain.SeedWork;

namespace Workout.API.Features.Serie
{
    public class UpdateSerieRepetitions
    {
        public record UpdateRepetitionsCommand(
            int Id,
            int Amount
            ) : IRequest;

        public class UpdateRepetitionsCommandValidator 
            : AbstractValidator<UpdateRepetitionsCommand>
        {
            protected readonly IUnitOfWork _unitOfWork;

            public UpdateRepetitionsCommandValidator(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;

                RuleFor(_ => _.Id)
                    .NotEmpty()
                    .MustAsync(ExistsAsync).WithMessage(_ => $"The serie with ID '{_.Id}' could not be found.");

                RuleFor(_ => _.Amount)
                    .NotEmpty()
                    .GreaterThan(0);
            }

            private async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
            {
                var serie = await _unitOfWork.Series.FindAsync(id);

                return serie is not null;
            }
        }

        public class UpdateRepetitionsCommandHandler
            : RequestHandlerBase, IRequestHandler<UpdateRepetitionsCommand>

        {
            public UpdateRepetitionsCommandHandler(IUnitOfWork workoutContext)
                : base(workoutContext) { }

            public async Task<Unit> Handle(UpdateRepetitionsCommand request, CancellationToken cancellationToken)
            {
                var serie = await _unitOfWork.Series.FindAsync(request.Id);

                serie!.UpdateRepetitions(request.Amount);
                _unitOfWork.Update(serie);

                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
