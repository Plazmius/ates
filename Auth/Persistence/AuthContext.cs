using System;
using Auth.Persistence.Models;
using Auth.Utils;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence
{
    public class AuthContext : IdentityDbContext<Popug,Role, Guid>
    {
        public DbSet<Popug> Popugs { get; set; }
        public AuthContext(DbContextOptions options) : base(options)
        {
            
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                // .UseNpgsql()
                .UseSnakeCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureSnakeCaseIdentity(modelBuilder);
        }

        private static void ConfigureSnakeCaseIdentity(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            //Rename Identity tables to lowercase
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var currentTableName = modelBuilder.Entity(entity.Name).Metadata.GetTableName();
                modelBuilder.Entity(entity.Name).ToTable(currentTableName.ToSnakeCase());
                Console.WriteLine(currentTableName.ToSnakeCase());
            }
        }
    }
}