using System.Collections.Generic;

namespace Speedrun.Models
{
    public class SRSystem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SRStrain> SRStrains { get; set; }
    }
}