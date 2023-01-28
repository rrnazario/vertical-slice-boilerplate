using MediatR;
using Microsoft.EntityFrameworkCore;
using Workout.API.SeedWork;
using Workout.Infra.Persistence;

namespace Workout.API.Features.Serie
{
    public class GetAllSeries
    {
        public record GetAllSeriesQuery 
            : IRequest<IEnumerable<GetAllSeriesQueryResponse>>;

        public record GetAllSeriesQueryResponse(
            QueryExerciseResponse Exercise,
            float Weight,
            int Repetitions
            );

        public record QueryExerciseResponse(
            string Name
            );

        public class QueryHandler
            : RequestHandlerBase, IRequestHandler<GetAllSeriesQuery, IEnumerable<GetAllSeriesQueryResponse>>
        {
            public QueryHandler(UnitOfWork workoutContext)
                : base(workoutContext) { }

            public async Task<IEnumerable<GetAllSeriesQueryResponse>> Handle(GetAllSeriesQuery request, CancellationToken cancellationToken)
            {
                var result = await _workoutContext
                    .Series
                    .Include(_ => _.Exercise)
                    .Select(s => new GetAllSeriesQueryResponse(
                        new QueryExerciseResponse(s.Exercise.Name),
                        s.Weight,
                        s.Repetitions))
                    .ToArrayAsync();
                
                return result.AsEnumerable();
            }
        }
    }
}
