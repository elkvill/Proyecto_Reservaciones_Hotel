using Hotel.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Data
{
    public class ApplicationDbContent : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContent(DbContextOptions<ApplicationDbContent> options) : base(options) { }

        public DbSet<Reserva> Reservas => Set<Reserva>();
        public DbSet<TipoHabitacion> TipoHabitaciones => Set<TipoHabitacion>();
        public DbSet<Habitacion> Habitaciones => Set<Habitacion>();
        public DbSet<DetalleReserva> DetalleReservas => Set<DetalleReserva>();

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ApplicationUser
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.NombreCompleto)
                      .IsRequired()
                      .HasMaxLength(75);

                entity.Property(e => e.FechaRegistro)
                      .IsRequired()
                      .HasColumnType("date");

                entity.Property(e => e.Estado)
                      .IsRequired()
                      .HasMaxLength(20);
            });

            // TipoHabitacion
            builder.Entity<TipoHabitacion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descripcion).HasMaxLength(250);

                // Nuevo Validar que el nombre del tipo de habitación sea único
                entity.HasIndex(e => e.Nombre).IsUnique();
            });

            // Habitacion
            builder.Entity<Habitacion>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

                entity.Property(e => e.Numero).IsRequired();
                entity.Property(e => e.Capacidad).IsRequired();
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20);

                // Nuevo Validar que no se repita el número de la habitación
                entity.HasIndex(e => e.Numero).IsUnique();

                entity.HasOne(e => e.TipoHabitacion)
                    .WithMany(t => t.Habitaciones)
                    .HasForeignKey(e => e.TipoHabitacionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Reserva
            builder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
                // Nuevo: Configurar la relación con ApplicationUser
                //entity.Property(e => e.UsuarioId).IsRequired();
                //entity.HasOne(e => e.Usuario)
                //      .WithMany()
                //      .HasForeignKey(e => e.UsuarioId)
                //      .OnDelete(DeleteBehavior.Restrict);

                // Configurar Cliente esto lo agregue hace poco, es para guardar el nombre del cliente que hizo la reserva,
                // aunque ya tengo el usuario relacionado, esto es para tener un campo adicional con el nombre del cliente por si acaso
                //entity.Property(e => e.Cliente).IsRequired().HasMaxLength(50);

                entity.Property(e => e.FechaInicio).IsRequired().HasColumnType("date");
                entity.Property(e => e.FechaFin).IsRequired().HasColumnType("date");
                entity.Property(e => e.Total).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20);
                entity.Property(e => e.FechaCreacion).IsRequired();

                // Nuevo para Validar que la fecha final sea posterior a la inicial 
                entity.ToTable(table =>
                {
                    table.HasCheckConstraint("CK_Reserva_Fechas", "\"FechaFin\" >= \"FechaInicio\"");
                    table.HasCheckConstraint("CK_Reserva_Estado", "\"Estado\" IN ('Pendiente', 'Confirmada', 'Cancelada', 'Completada')");
                });

                //Relación con con el modelo Application useresto lo agregue hace poco, es para relacionar la reserva con el usuario que la hizo
                entity.HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
                
            });

            // DetalleReserva
            builder.Entity<DetalleReserva>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

                entity.Property(e => e.PrecioPorNoche).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Subtotal).HasColumnType("decimal(18,2)");

                // Validar que una misma habitación no se pueda registrar dos veces en la misma reserva
                entity.HasIndex(e => new { e.ReservaId, e.HabitacionId }).IsUnique();

                entity.HasOne(e => e.Reserva)
                    .WithMany(r => r.DetalleReservas)
                    .HasForeignKey(e => e.ReservaId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Habitacion)
                    .WithMany(h => h.DetalleReservas)
                    .HasForeignKey(e => e.HabitacionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
