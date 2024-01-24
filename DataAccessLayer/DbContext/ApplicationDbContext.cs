using DataAccessLayer.Entities;
using DataAccessLayer.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DataAccessLayer.DbContext
{
    /// <summary>
    /// Class for registration our models in context application,setting their and change setting connection to BD 
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<UserProfile> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Entities.Task> Tasks { get; set; }


        /// <summary>
        /// Method from Fluent API give possibility setting models and link between models
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<Guid>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });

            //set link beetwen user and categories and category and task
            modelBuilder.Entity<UserProfile>()
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

            modelBuilder.Entity<ApplicationUser>()
               .HasOne(u => u.UserProfile)        
               .WithOne(up => up.ApplicationUser)              
               .HasForeignKey<UserProfile>(up => up.UserProfileId)  
               .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
