using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Entities.Task> Tasks { get; set; }


        /// <summary>
        /// Method from Fluent API give possibility setting models and link between models
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //set link one to many between user and role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.RoleId);

            //set link beetwen user and categories and category and task
            modelBuilder.Entity<User>()
                .HasMany(u => u.Categories)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //set link beetwen category and task
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Tasks)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            //Seed date
            string rolesJson = File.ReadAllText("roles.json");
            List<Role> roles = JsonSerializer.Deserialize<List<Role>>(rolesJson);

            //Seed unique username for user
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            //seed unique name for category
            modelBuilder.Entity<Category>()
                .HasIndex(u => u.Name)
                .IsUnique();

            //seed unique name for role
            modelBuilder.Entity<Role>()
                .HasIndex(u => u.Name)
                .IsUnique();

            foreach (var role in roles)
                modelBuilder.Entity<Role>().HasData(role);
        }
    }
}
