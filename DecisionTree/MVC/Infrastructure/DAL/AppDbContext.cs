using DecisionTree.MVC.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DecisionTree.MVC.Infrastructure.DAL
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<HierarchyItem> HierarchyItemSet { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            // create self-reference model binding
            //builder.Entity<HierarchyItem>()
            //    .HasMany(p => p.Children)
            //    .WithOne(c => c.Parent)
            //    .HasForeignKey(c => c.ParentId)
            //    .IsRequired(false);

            builder.Entity<HierarchyItem>()
                .HasOne(p => p.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId)
                .IsRequired(false);


            // bind model collection to json array
            builder.Entity<HierarchyItem>().OwnsMany(p =>
                p.MetadataList, ownedNavigationBuilder =>
                {
                    ownedNavigationBuilder.ToJson();
                });

            // bind model to json object
            builder.Entity<HierarchyItem>().OwnsOne(p =>
                p.Metadata, ownedNavigationBuilder =>
                {
                    ownedNavigationBuilder.ToJson();
                });


            // auto eager loading 
            //builder.Entity<HierarchyItem>().Navigation(e => e.Children).AutoInclude();
        }
    }
}