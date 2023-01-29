using FluentAssertions;

namespace Workout.UnitTests.Serie
{
    public class UpdateSerieRepetitionTests
    {
        [Fact]
        public void UpdateSerieRepetitionWithDifferentValues()
        {
            var serie = new Domain.Model.Serie(1, 1, repetitions: 1, 1);

            serie.UpdateRepetitions(2);

            serie.Repetitions.Should().Be(2);
        }

        [Fact]
        public void UpdateSerieRepetitionWithSameValue_NothingChanges()
        {
            var serie = new Domain.Model.Serie(1, 1, repetitions: 1, 1);

            serie.UpdateRepetitions(1);

            serie.Repetitions.Should().Be(1);
        }
    }
}
