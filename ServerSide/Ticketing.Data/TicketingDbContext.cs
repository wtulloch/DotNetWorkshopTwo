using Microsoft.EntityFrameworkCore;
using Ticketing.Models.DbModels;

namespace Ticketing.Data
{
    public partial class TicketingDbContext : DbContext
    {
        public TicketingDbContext()
        {
        }

        public TicketingDbContext(DbContextOptions<TicketingDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Production> Productions { get; set; }
        public virtual DbSet<Show> Shows { get; set; }
        public virtual DbSet<Theatre> Theatres { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<UserTicket> UserTickets { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Production>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.TicketSuffix)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.HasOne(d => d.Theatre)
                    .WithMany(p => p.Productions)
                    .HasForeignKey(d => d.TheatreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Productions_Theatres");
            });


            modelBuilder.Entity<Show>(entity =>
            {
                entity.HasOne(d => d.Production)
                    .WithMany(p => p.Shows)
                    .HasForeignKey(d => d.ProductionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shows_Productions");
            });

            modelBuilder.Entity<Theatre>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.TicketPrefix)
                    .IsRequired()
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.TicketNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Show)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.ShowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_Shows");
            });

            modelBuilder.Entity<UserTicket>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.Ticket)
                    .WithMany()
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTickets_Tickets");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTickets_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(125);

                entity.Property(e => e.FamilyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GivenName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(125);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
