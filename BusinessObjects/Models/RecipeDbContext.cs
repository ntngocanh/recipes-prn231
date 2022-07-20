using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class RecipeDbContext : DbContext
    {
        public RecipeDbContext() { }
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options)
            : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("RecipeDB"));
        }
        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<CollectionRecipe> CollectionRecipes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Step> Steps { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Reaction> Reactions { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<FeaturedTag> FeaturedTags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reaction>().HasKey(r => new { r.UserId, r.RecipeId });
            modelBuilder.Entity<CollectionRecipe>().HasKey(r => new { r.CollectionId, r.RecipeId });
            modelBuilder.Entity<CollectionRecipe>().HasOne(cr => cr.Collection).WithMany(c => c.CollectionRecipes).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<Comment>().HasOne(c => c.Recipe).WithMany(c => c.Comments).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<Reaction>().HasOne(c => c.Recipe).WithMany(c => c.Reactions).OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}
