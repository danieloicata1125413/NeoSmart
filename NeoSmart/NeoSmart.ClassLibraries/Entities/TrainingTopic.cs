namespace NeoSmart.ClassLibraries.Entities
{
    public class TrainingTopic
    {
        public int Id { get; set; }

        public Training Training { get; set; } = null!;

        public int TrainingId { get; set; }

        public Topic Topic { get; set; } = null!;

        public int TopicId { get; set; }
    }
}