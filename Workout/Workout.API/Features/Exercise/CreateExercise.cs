using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Workout.API.SeedWork;
using Workout.Domain.SeedWork;

namespace Workout.API.Features.Exercise
{
    public class CreateExercise
    {
        public record CreateExerciseCommand(
            string Name,
            string Description
            ) : IRequest<int>;

        public class CreateExerciseCommandValidator 
            : AbstractValidator<CreateExerciseCommand>
        {
            protected readonly IUnitOfWork _unitOfWork;
            public CreateExerciseCommandValidator(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;

                RuleFor(_ => _.Name)
                    .NotEmpty()
                    .MaximumLength(200).WithMessage("Name has exceeded 200 characters")
                    .MustAsync(BeUniqueNameAsync).WithMessage("Name must be unique");                
            }

            private async Task<bool> BeUniqueNameAsync(string name, CancellationToken cancellationToken)
            {
                var exercise = await _unitOfWork.Exercises.FirstOrDefaultAsync(_ => _.Name.Equals(name), cancellationToken);

                return exercise is null;
            }
        }

        public record CreatedExerciseResponse(
            int Id);

        public class Handler
            : RequestHandlerBase, IRequestHandler<CreateExerciseCommand, int>
        {
            public Handler(IUnitOfWork unitOfWork)
                : base(unitOfWork) { }

            public async Task<int> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
            {
                var newExercise = new Domain.Model.Exercise(request.Name, request.Description, string.Empty);

                var result = await _unitOfWork.Exercises.AddAsync(newExercise, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return result.Entity.Id;
            }
        }
    }
}
