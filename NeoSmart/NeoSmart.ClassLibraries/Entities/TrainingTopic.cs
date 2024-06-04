namespace NeoSmart.ClassLibraries.Entities
{
    public class TrainingTopic
    {
        public int Id { get; set; }

        public int TrainingId { get; set; }

        public Training? Training { get; set; } = null!;

        public int TopicId { get; set; }

        public Topic? Topic { get; set; } = null!;
    }
}