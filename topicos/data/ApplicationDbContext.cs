
using Microsoft.EntityFrameworkCore;

namespace topicos.data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies(); // Habilita Lazy Loading
        }
        public DbSet<PlanDeEstudio> PlanesDeEstudio { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Prerequisito> Prerequisitos { get; set; }

        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación entre PlanDeEstudio y Materia
            modelBuilder.Entity<PlanDeEstudio>()
                .HasMany(p => p.Materias)
                .WithOne(m => m.PlanDeEstudio)
                .HasForeignKey(m => m.PlanDeEstudioId);

            // Relación entre Materia y Prerequisito (Materia como prerequisito de otras)
            modelBuilder.Entity<Prerequisito>()
                .HasOne(p => p.Materia)
                .WithMany(m => m.Prerequisitos) // Materia puede tener muchos prerequisitos
                .HasForeignKey(p => p.MateriaId)
                .OnDelete(DeleteBehavior.Restrict); // No permitir eliminar materia si es prerequisito

            // Relación inversa: Una materia puede ser prerequisito de muchas otras
            modelBuilder.Entity<Prerequisito>()
                .HasOne(p => p.MateriaRequisito)
                .WithMany(m => m.EsRequisitoDe) // MateriaRequisito tiene muchos prerequisitos
                .HasForeignKey(p => p.MateriaRequisitoId)
                .OnDelete(DeleteBehavior.Restrict); // No permitir eliminar una materia si tiene dependencias
        }
    }
}
