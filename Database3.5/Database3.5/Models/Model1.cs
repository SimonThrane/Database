using System.Data.Entity;

namespace Database3._5.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Adresse> Adresses { get; set; }
        public virtual DbSet<erPaa> erPaas { get; set; }
        public virtual DbSet<har> hars { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Telefon> Telefons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adresse>()
                .HasMany(e => e.People)
                .WithRequired(e => e.Adresse)
                .WillCascadeOnDelete(false);
        }
    }
}
