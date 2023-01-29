using MediatR;
using Microsoft.EntityFrameworkCore;
using Workout.API.SeedWork;
using Workout.Domain.SeedWork;

namespace Workout.API.Features.Serie
{
    public class GetAllSeries
    {
        public record GetAllSeriesQuery 
            : IRequest<IEnumerable<GetAllSeriesQueryResponse>>;

        public record GetAllSeriesQueryResponse
        {
            public QueryExerciseResponse Exercise { get; }
            public float Weight { get; }
            public int Repetitions { get; }

            public GetAllSeriesQueryResponse(string exerciseName, float weight, int repetitions)
            {
                Exercise = new(exerciseName);
                Weight = weight;
                Repetitions = repetitions;
            }
        }

        public record QueryExerciseResponse(string Name);

        public class QueryHandler
            : RequestHandlerBase, IRequestHandler<GetAllSeriesQuery, IEnumerable<GetAllSeriesQueryResponse>>
        {
            public QueryHandler(IUnitOfWork unitOfWork)
                : base(unitOfWork) { }

            public async Task<IEnumerable<GetAllSeriesQueryResponse>> Handle(GetAllSeriesQuery request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork
                    .Series
                    .Include(_ => _.Exercise)
                    .Select(s => new GetAllSeriesQueryResponse(
                        s.Exercise.Name,
                        s.Weight,
                        s.Repetitions))
                    .ToArrayAsync(cancellationToken);
                
                return result.AsEnumerable();
            }
        }
    }
}
