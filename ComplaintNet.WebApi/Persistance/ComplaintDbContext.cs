using ComplaintNet.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ComplaintNet.WebApi.Persistance
{
    public class ComplaintDbContext : DbContext
    {
        public ComplaintDbContext(DbContextOptions<ComplaintDbContext> options)
        : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Complaint> Complaints { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
