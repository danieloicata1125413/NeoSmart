using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSmart.ClassLibraries.Entities
{
    public class FormationOccupation
    {
        public int Id { get; set; }

        public Formation Formation { get; set; } = null!;

        public int FormationId { get; set; }

        public Occupation Occupation { get; set; } = null!;

        public int OccupationId { get; set; }
    }
}
