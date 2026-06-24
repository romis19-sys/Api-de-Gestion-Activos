using GestionActivos.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionActivos.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Clientes> Clientes => Set<Clientes>();
        public DbSet<Prestamos> Prestamos => Set<Prestamos>();
        public DbSet<Multas> Multas => Set<Multas>();
        public DbSet<Pagos> Pagos => Set<Pagos>();
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<DetallesMultas> DetallesMultas => Set<DetallesMultas>();
        public DbSet<DetallesPagos> DetallesPagos => Set<DetallesPagos>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Usuarios 
            builder.Entity<ApplicationUser>(static entity =>
            {
                entity.Property(e => e.NombreCompleto).IsRequired().HasMaxLength(75);
            });

            // Clientes
            builder.Entity<Clientes>(entity =>
            {
                entity.HasKey(e => e.ClienteId);
                entity.Property(e => e.Dni).IsRequired().HasMaxLength(20);
                entity.Property(e => e.NombreCompleto).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Direccion).HasMaxLength(250);
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20);

                // agregamos la restriccion de estado, "Activo", "Inactivo"
                entity.ToTable(t => t.HasCheckConstraint("CK_Clientes_Estado", "\"Estado\" IN ('Activo', 'Inactivo')"));
            });

            // Prestamos
            builder.Entity<Prestamos>(entity =>
            {
                entity.HasKey(e => e.PrestamoId);
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Monto).HasColumnType("decimal(18,2)");
                entity.Property(e => e.MontoCuota).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Cliente)
                    .WithMany(c => c.Prestamos)
                    .HasForeignKey(e => e.ClienteId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ApplicationUser)
                    .WithMany(u => u.Prestamos)
                    .HasForeignKey(e => e.ApplicationUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                // faltan los estados del prestamo, "Pagado", "Cancelado"
                entity.ToTable(t => t.HasCheckConstraint("CK_Prestamos_Estado", "\"Estado\" IN ('Pendiente', 'Pagado', 'Cancelado')"));
            });

            // Multas
            builder.Entity<Multas>(entity =>
            {
                entity.HasKey(e => e.MultasId);
                entity.Property(e => e.Codigo).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Monto).HasColumnType("decimal(18,2)");
                entity.Property(e => e.TipoMulta).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20);

                entity.HasOne(e => e.Prestamo)
                    .WithMany(p => p.Multas)
                    .HasForeignKey(e => e.PrestamoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ApplicationUser)
                    .WithMany(u => u.Multas)
                    .HasForeignKey(e => e.ApplicationUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                // faltan los estados de la multa igual
                entity.ToTable(t => t.HasCheckConstraint("CK_Multas_Estado", "\"Estado\" IN ('Pendiente', 'Pagado', 'Anulada')"));
            });

            // Pagos
            builder.Entity<Pagos>(entity =>
            {
                entity.HasKey(e => e.PagoId);
                entity.Property(e => e.MontoPagado).HasColumnType("decimal(18,2)");
                entity.Property(e => e.MetodoPago).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20);

                entity.HasOne(e => e.Prestamo)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(e => e.PrestamoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Multa)
                    .WithMany(m => m.Pagos)
                    .HasForeignKey(e => e.MultasId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ApplicationUser)
                    .WithMany(u => u.Pagos)
                    .HasForeignKey(e => e.ApplicationUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                // los estados de los pagos igualmente
                entity.ToTable(t => t.HasCheckConstraint("CK_Pagos_Estado", "\"Estado\" IN ('Completado', 'Pendiente', 'Cancelado')"));
            });

            // DetallesMultas
            builder.Entity<DetallesMultas>(entity =>
            {
                entity.HasKey(e => e.DetallesMultaId);
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Gravedad).IsRequired().HasMaxLength(30);

                entity.HasOne(e => e.Multa)
                    .WithMany(m => m.DetallesMultas)
                    .HasForeignKey(e => e.MultasId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Prestamo)
                    .WithMany(p => p.DetallesMultas)
                    .HasForeignKey(e => e.PrestamoId)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            // DetallesPagos
            builder.Entity<DetallesPagos>(entity =>
            {
                entity.HasKey(e => e.DetallesPagoId);
                entity.Property(e => e.Concepto).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Monto).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Descripcion).HasMaxLength(250);

                entity.HasOne(e => e.Pago)
                    .WithMany(p => p.DetallesPagos)
                    .HasForeignKey(e => e.PagoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}