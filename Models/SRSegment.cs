namespace Speedrun.Models
{
    public class SRSegment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MsDuration { get; set; }
        public int SRStrainId { get; set; }
        public SRStrain SRStrain { get; set; }
    }
}