using Light.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Workout.API.Features.Exercise.CreateExercise;
using static Workout.API.Features.Exercise.GetAllExercises;

namespace Workout.API.Features.Exercise
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly IMediator mediator;

        public ExerciseController(IMediator mediator)
        {
            this.mediator = mediator.MustNotBeNull();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllExercisesResponse>))]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await mediator.Send(new GetAllExercisesQuery()));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedExerciseResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateExerciseCommand command)
        {
            var newId = await mediator.Send(command);

            return Ok(new CreatedExerciseResponse(newId));
        }
    }
}
