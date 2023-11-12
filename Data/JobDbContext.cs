using JobSearch.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace JobSearch.Data
{
    public class JobDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Offer> Offers { get; set; }

        public JobDbContext(DbContextOptions<JobDbContext> options):base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Field>().HasData(
                new Field() { Id = 1,Name="IT" },
                new Field() { Id = 2, Name="HR"}
                ) ;
        }

    }
}
