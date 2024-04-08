namespace NeoSmart.ClassLibraries.Entities
{
    public class FormationTopic
    {
        public int Id { get; set; }

        public Formation? Formation { get; set; } = null!;

        public int FormationId { get; set; }

        public Topic? Topic { get; set; } = null!;

        public int TopicId { get; set; }
    }
}