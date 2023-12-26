using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Class for registration our models in context application,setting their and change setting connection to BD 
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Method from Fluent API give possibility setting models and link between models
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.RoleId);

            //Seed date
            string rolesJson = File.ReadAllText("roles.json");
            List<Role> roles = JsonSerializer.Deserialize<List<Role>>(rolesJson);

            //Seed unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            foreach (var role in roles)
                modelBuilder.Entity<Role>().HasData(role);
        }
    }
}
