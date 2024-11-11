using Microsoft.EntityFrameworkCore;
using Net_6_Assignment.Models;

namespace Net_6_Assignment.Data
{
    public class A6DbContext:DbContext
    {
        public virtual DbSet<Category> DBCategory { get; set; }
        public virtual DbSet<Course> DBCourse { get; set; }
        public virtual DbSet<User> DBUser { get; set; }
        public A6DbContext(DbContextOptions<A6DbContext> options) : base(options)
        {
        }
        public A6DbContext() { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            { 
                b.ToTable("Users");
                b.HasKey(x => x.Id);
                b.Property(x => x.UserName).IsRequired().HasMaxLength(50);
                b.HasMany(x => x.Courses); 
            });
            modelBuilder.Entity<Category>(b =>
            {
                b.ToTable("Categories");
                b.HasKey(x => x.Id);
                b.Property(x => x.CategoryName).IsRequired().HasMaxLength(50);
                b.HasMany(x => x.Courses)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Course>(b =>
            {
                b.ToTable("Courses");
                b.HasKey(x => x.Id);
                b.Property(x => x.CourseName).IsRequired().HasMaxLength(50);
                b.Property(x => x.Description).IsRequired().HasMaxLength(200);
                b.HasOne(x => x.Category)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
                b.HasOne(x => x.User)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
