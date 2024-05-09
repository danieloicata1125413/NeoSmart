using NeoSmart.ClassLibraries.Entities;

namespace NeoSmart.ClassLibraries.DTOs
{
    public class TrainingTopicDTO
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public Topic? Topic { get; set; } = null!;
    }
}
