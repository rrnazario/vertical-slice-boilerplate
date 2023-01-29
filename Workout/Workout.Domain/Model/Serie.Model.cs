using System.Text.Json.Serialization;
using Workout.Domain.SeedWork;

namespace Workout.Domain.Model
{
    public class Serie
        : Entity
    {
        public int ExerciseId { get; private set; }
        public int Repetitions { get; private set; }
        public float Weight { get; private set; }

        [JsonIgnore]
        public Exercise Exercise { get; private set; }

        public Serie(int id, int exerciseId, int repetitions, float weight)
        {
            Id = id;
            ExerciseId = exerciseId;
            Repetitions = repetitions;
            Weight = weight;
        }

        public void UpdateRepetitions(int newValue)
        {
            if (newValue != Repetitions)
                Repetitions = newValue;
        }
    }
}
