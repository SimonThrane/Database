using System.Data.Entity;

namespace GDL.Model
{
  class GDLContext : DbContext
  {

    public GDLContext()
        : base("DefaultConnection")
    {
    }

    public DbSet<CharacteristicContainer> CharacteristicContainers { get; set; }
    public DbSet<ReadingContainer> ReadingContainers { get; set; }
  }
}
