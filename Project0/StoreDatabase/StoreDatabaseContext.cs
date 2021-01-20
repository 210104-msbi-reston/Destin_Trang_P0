// using System;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata;

// #nullable disable

// namespace StoreDatabase
// {
//     public partial class StoreDatabaseContext : DbContext
//     {

//         public StoreDatabaseContext () {

//         }

//         public StoreDatabaseContext (DbContextOptions<StoreDatabaseContext> options) : base (options) {

//         }

//         //Declaring the tables we'll be using
//         public virtual DbSet<Location> LocationTable { get; set; }
//         public virtual DbSet<Product> ProductTable { get; set; }
//         public virtual DbSet<Inventory> InventoryTable { get; set; }

//         protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
//             if (!optionsBuilder.IsConfigured) {
//                 optionsBuilder.UseSqlServer("server=DESTIN\\TRAININGSERVER;database=Project0DB ;integrated security=true");
//             }
//         }

//         protected override void OnModelCreating (ModelBuilder modelBuilder)
//         {

//             modelBuilder.HasAnnotation ("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

//             modelBuilder.Entity<Product> (entity =>
//             {
                
//             });

//         }

//     }
// }