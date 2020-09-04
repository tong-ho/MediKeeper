using Microsoft.EntityFrameworkCore;

namespace MediKeeper.RestApi.Entities
{
    public partial class MediKeeperContext : DbContext
    {
        public MediKeeperContext()
        {
        }

        public MediKeeperContext(DbContextOptions<MediKeeperContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Item> Item { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
