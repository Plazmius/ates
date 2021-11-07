using System;
using Auth.Persistence.Models;
using Auth.Utils;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence
{
    public class AuthContext : IdentityDbContext<Popug,Role, ulong>
    {
        
        public AuthContext(DbContextOptions options) : base(options)
        {
            
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                // .UseNpgsql()
                .UseSnakeCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);

            //Rename Identity tables to lowercase
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var currentTableName = modelBuilder.Entity(entity.Name).Metadata.GetTableName();
                modelBuilder.Entity(entity.Name).ToTable(currentTableName.ToSnakeCase());
                Console.WriteLine(currentTableName.ToSnakeCase());
            }
            
            modelBuilder.Entity<Popug>()
                .Property(p => p.PublicId)
                .HasDefaultValueSql("uuid_generate_v4()");
            
            
        }
    }
}