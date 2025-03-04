using Microsoft.EntityFrameworkCore;

namespace Speedrun.Models
{
    public class SRDbContext : DbContext
    {
        public SRDbContext(DbContextOptions<SRDbContext> options) : base(options) { }

        public DbSet<SRSystem> SRSystem { get; set; }
        public DbSet<SRStrain> SRStrain { get; set; }
        public DbSet<SRSegment> SRSegment { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SRStrain>()
                .HasOne(s => s.SRSystem)
                .WithMany(s => s.SRStrains)
                .HasForeignKey(s => s.SRSystemId);

            base.OnModelCreating(modelBuilder);
        }
        // public object SRSystems { get; internal set; }
    }
}