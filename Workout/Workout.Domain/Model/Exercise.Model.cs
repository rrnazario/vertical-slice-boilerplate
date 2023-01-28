using System.Text.Json.Serialization;
using Workout.Domain.SeedWork;

namespace Workout.Domain.Model
{
    public class Exercise
        : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Medias { get; private set; }

        private Exercise() { }
        
        public Exercise(string name, string description, string medias)            
        {
            Name = name;
            Description = description;
            Medias = medias;
        }

        public Exercise(int id, string name, string description, string medias)
            : this(name, description, medias)
        {
            Id = id;            
        }

        public Exercise(int id, string name, string description)
            : this(id, name, description, string.Empty)
        {
        }

    }
}
