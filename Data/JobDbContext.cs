using JobSearch.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace JobSearch.Data
{
    public class JobDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public JobDbContext(DbContextOptions<JobDbContext> options):base(options) { }
        
    }
}
