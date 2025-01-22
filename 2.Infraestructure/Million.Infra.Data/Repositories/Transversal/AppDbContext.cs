using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Million.Domain.Entities.Model.Operation;
using Million.Domain.Entities.Model.Transversal;
using System;
using System.IO;

namespace Million.Infra.Data.Repositories.Transversal
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
                
        public DbSet<User> Users { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyTrace> PropertyTraces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Owner>().HasKey(x => x.IdOwner);
            modelBuilder.Entity<Property>().HasKey(x => x.IdProperty);
            modelBuilder.Entity<PropertyImage>().HasKey(x => x.IdPropertyImage);
            modelBuilder.Entity<PropertyTrace>().HasKey(x => x.IdPropertyTrace);

            modelBuilder.Entity<Property>()                
                .HasOne(o => o.Owner)
                .WithMany(u => u.Properties)
                .HasForeignKey(o => o.IdOwner);

            modelBuilder.Entity<PropertyImage>()
                .HasOne(o => o.Property)
                .WithMany(p => p.PropertyImages)
                .HasForeignKey(o => o.IdProperty);

            modelBuilder.Entity<PropertyTrace>()
                .HasOne(o => o.Property)
                .WithMany(p => p.PropertyTraces)
                .HasForeignKey(o => o.IdProperty);

            base.OnModelCreating(modelBuilder);
        }
    }

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configurationFilePath = Path.Combine(Directory.GetCurrentDirectory(), "../..", "4.WebApi", "Million.WebApi", "appsettings.json");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(configurationFilePath)!)
                .AddJsonFile(Path.GetFileName(configurationFilePath))
                .Build();

            string connectionString = configuration["AppSettings:DefaultConnection"];

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("string connection not found in appSettings million");
            }

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }


}
