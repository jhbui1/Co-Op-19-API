using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoOp19API.Models
{
    public partial class CoreDbContext : DbContext
    {
        private string access;
        public CoreDbContext(string access)
        {
            this.access = access;
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
                optionsBuilder.UseSqlServer(access);
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConsumableResource>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ResourceId).HasColumnName("Resource_ID");


                entity.HasOne(d => d.Resource)
                      .WithMany(p => p.ConsumableResource)
                      .HasForeignKey(d => d.ResourceId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK__Consumabl__Resou__571DF1D5");
            });

            modelBuilder.Entity<GenericResource>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.LocId).HasColumnName("LocID");

                entity.Property(e => e.Title)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.HasOne(d => d.Loc)
                   .WithMany(p => p.GenericResource)
                   .HasForeignKey(d => d.LocId)
                   .HasConstraintName("FK__GenericRe__LocID__5441852A");

            });

            modelBuilder.Entity<HealthResource>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ResourceId).HasColumnName("Resource_ID");

                entity.Property(e => e.TestPrice).HasColumnType("money");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.HealthResource)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HealthRes__Resou__5AEE82B9");
            });

            modelBuilder.Entity<HealthResourceServices>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EstCost).HasColumnType("money");

                entity.Property(e => e.ResourceId).HasColumnName("HealthRes");

                entity.Property(e => e.ServiceDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.HealthResNavigation)
                    .WithMany(p => p.HealthResourceServices)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HealthRes__Recou__6FE99F9F");
            });

            modelBuilder.Entity<MapData>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .IsFixedLength();

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Gpsn)
                    .HasColumnName("GPSN")
                    .HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Gpsw)
                    .HasColumnName("GPSW")
                    .HasColumnType("decimal(9, 6)");

                entity.Property(e => e.State)
                    .HasMaxLength(40)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ShelterResource>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ResourceId).HasColumnName("Resource_ID");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.ShelterResource)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ShelterRe__Resou__619B8048");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasName("UQ__Users__C9F284564D1F27D0")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("FName")
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Lname)
                    .HasColumnName("LName")
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Phone).HasColumnType("decimal(15, 0)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.HasOne(d => d.LocNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Loc)
                    .HasConstraintName("FK__Users__Loc__4D94879B");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
