using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoOp19API.Models
{
    public partial class CoreDbContext : DbContext
    {
        public CoreDbContext()
        {
        }

        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ConsumableResource> ConsumableResource { get; set; }
        public virtual DbSet<GenericResource> GenericResource { get; set; }
        public virtual DbSet<HealthResource> HealthResource { get; set; }
        public virtual DbSet<HealthResourceServices> HealthResourceServices { get; set; }
        public virtual DbSet<MapData> MapData { get; set; }
        public virtual DbSet<ShelterResource> ShelterResource { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-3AOSQNV\\MSSQLSERVER01;Database=coop19;trusted_connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConsumableResource>(entity =>
            {
                entity.HasOne(d => d.Loc)
                    .WithMany(p => p.ConsumableResource)
                    .HasForeignKey(d => d.LocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Consumabl__Loc_I__300424B4");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.ConsumableResource)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Consumabl__Resou__2F10007B");
            });

            modelBuilder.Entity<GenericResource>(entity =>
            {
                entity.HasIndex(e => e.Title)
                    .HasName("UQ__GenericR__2CB664DC0531DFCE")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Comment).IsFixedLength();

                entity.Property(e => e.Title).IsFixedLength();

                entity.HasOne(d => d.Loc)
                    .WithOne(p => p.GenericResource)
                    .HasForeignKey<GenericResource>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GenericResou__ID__2C3393D0");
            });

            modelBuilder.Entity<HealthResource>(entity =>
            {
                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.HealthResource)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HealthRes__Resou__33D4B598");
            });

            modelBuilder.Entity<HealthResourceServices>(entity =>
            {
                entity.Property(e => e.ServiceDesc).IsUnicode(false);

                entity.Property(e => e.ServiceName).IsUnicode(false);

                entity.HasOne(d => d.HealthResNavigation)
                    .WithMany(p => p.HealthResourceServices)
                    .HasForeignKey(d => d.HealthRes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HealthRes__Healt__37A5467C");
            });

            modelBuilder.Entity<MapData>(entity =>
            {
                entity.Property(e => e.Address).IsFixedLength();

                entity.Property(e => e.City).IsFixedLength();

                entity.Property(e => e.State).IsFixedLength();
            });

            modelBuilder.Entity<ShelterResource>(entity =>
            {
                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.ShelterResource)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ShelterRe__Resou__3B75D760");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasName("UQ__Users__C9F2845681327F88")
                    .IsUnique();

                entity.Property(e => e.Email).IsFixedLength();

                entity.Property(e => e.Fname).IsFixedLength();

                entity.Property(e => e.Lname).IsFixedLength();

                entity.Property(e => e.Password).IsFixedLength();

                entity.Property(e => e.UserName).IsFixedLength();

                entity.HasOne(d => d.LocNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Loc)
                    .HasConstraintName("FK__Users__Loc__286302EC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
