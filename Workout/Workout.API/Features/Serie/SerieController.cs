using Light.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Workout.API.Features.Serie.GetAllSeries;
using static Workout.API.Features.Serie.UpdateSerieRepetitions;

namespace Workout.API.Features.Serie
{
    [ApiController]
    [Route("api/[controller]")]
    public class SerieController 
        : ControllerBase
    {
        private readonly IMediator mediator;

        public SerieController(IMediator mediator)
        {
            this.mediator = mediator.MustNotBeNull();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllSeriesQueryResponse))]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await mediator.Send(new GetAllSeriesQuery()));
        }

        [HttpPatch("{id}/Repetitions/{amount}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRepetitions([FromRoute] int id, [FromRoute] int amount)
        {
            await mediator.Send(new UpdateRepetitionsCommand(id, amount));
            
            return NoContent();
        }
    }
}
