using System.Collections.Generic;

namespace Speedrun.Models
{
    public class SRStrain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SRSystemId { get; set; }
        public SRSystem SRSystem { get; set; }
        public ICollection<SRSegment> SRSegments { get; set; }
    }
}