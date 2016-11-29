using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDL.Model
{
    class GDLContext : DbContext
    {
        public DbSet<CharacteristicContainer> CharacteristicContainers { get; set; }
        public DbSet<ReadingContainer> ReadingContainers { get; set; }
    }
}
