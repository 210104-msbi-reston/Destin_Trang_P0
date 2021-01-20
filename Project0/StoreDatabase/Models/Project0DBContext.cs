using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace StoreDatabase.Models
{
    public partial class Project0DBContext : DbContext
    {
        public Project0DBContext()
        {
        }

        public Project0DBContext(DbContextOptions<Project0DBContext> options)
            : base(options)
        {
        }

        //Declaring the tables we'll be using
        public virtual DbSet<Location> LocationTable { get; set; }
        public virtual DbSet<Product> ProductTable { get; set; }
        public virtual DbSet<Inventory> InventoryTable { get; set; }
        public virtual DbSet<Order> OrderTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=DESTIN\\TRAININGSERVER;database=Project0DB;integrated security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");


            modelBuilder.Entity<Product> (entity =>
            {
                entity.HasKey(e => e.productID);
                entity.ToTable("Products");

                entity.Property(e => e.productID).HasColumnName("productID");
                entity.Property(e => e.name).HasColumnName("name");
                entity.Property(e => e.price).HasColumnName("price");
            });

            modelBuilder.Entity<Location> (entity => 
            {
                entity.HasKey(e => e.storeID);
                entity.ToTable("Locations");

                entity.Property(e => e.storeID).HasColumnName("storeID");
                entity.Property(e => e.city).HasColumnName("location");
            });

            modelBuilder.Entity<Inventory> (entity => 
            {
                entity.HasKey(e => new {e.storeID, e.productID});
                entity.ToTable("Inventory");

                entity.Property(e => e.storeID).HasColumnName("storeID");
                entity.Property(e => e.productID).HasColumnName("productID");
                entity.Property(e => e.quantity).HasColumnName("quantity");
                // entity
                //     .HasOne(d => d.location);
                // entity
                //     .HasOne(d => d.product)
                //     .WithMany(p => p.inventories)
                //     .HasForeignKey(d => d.productID);
            });

            modelBuilder.Entity<Order> (entity =>
            {
                entity.ToTable("Orders");

                entity.Property(e => e.orderID).HasColumnName("orderID");
                entity.Property(e => e.email).HasColumnName("email");
                entity.Property(e => e.storeID).HasColumnName("storeID");
                entity.Property(e => e.productID).HasColumnName("productID");
                entity.Property(e => e.quantity).HasColumnName("quantity");
                entity.Property(e => e.date).HasColumnType("datetime").HasColumnName("date");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
