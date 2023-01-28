using Light.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            : IRequestHandler<GetAllSeriesQuery, IEnumerable<GetAllSeriesQueryResponse>>
        {
            private readonly WorkoutContext _workoutContext;

            public QueryHandler(WorkoutContext workoutContext)
            {
                _workoutContext = workoutContext.MustNotBeNull();
            }

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
