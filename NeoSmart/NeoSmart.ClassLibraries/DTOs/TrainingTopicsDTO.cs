using NeoSmart.ClassLibraries.Interfaces;

namespace NeoSmart.ClassLibraries.DTOs
{
    public class TrainingTopicsDTO : ISoftDetete
    {
        public int Id { get; set; }

        public List<int>? TrainingTopicIds { get; set; }

        public ICollection<TrainingTopicDTO>? TrainingTopics { get; set; }

    }
}
