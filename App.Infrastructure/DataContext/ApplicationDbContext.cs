using App.Domain.Entities;
using App.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace App.Infrastructure.DataContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> op) : base(op) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Verb> Verbs { get; set; }
        public DbSet<SentenceStructure> SentenceStructures { get; set; }
        public DbSet<SentenceForms> SentenceFormss { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Category> Categorys { get; set; }   
        public DbSet<SentenceFormStructureMapping> FormStructureMappings { get; set; }
        public DbSet<SubCategoryFormMapping> SubCategoryFormMappings { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            // SubCategory to SentenceForm (One-to-Many)
            builder.Entity<SentenceFormStructureMapping>()
            .HasOne(s => s.SentenceForm)
            .WithMany(s => s.SentenceFormStructureMapping)
            .HasForeignKey(s => s.FormateID);

            builder.Entity<SentenceFormStructureMapping>()
                .HasOne(s => s.SentenceStructure)
                .WithMany() // Assuming SentenceStructure does not need a navigation property back to mappings
                .HasForeignKey(s => s.StructureID);
        }
    }
}
