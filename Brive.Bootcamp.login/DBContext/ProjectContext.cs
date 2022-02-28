using Brive.Bootcamp.login.Models;
using Microsoft.EntityFrameworkCore;

namespace Brive.Bootcamp.login.DBContext
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }
           
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(eb =>
            {
                eb.HasKey(c => new { c.Id });
            });
        }

        public DbSet<Users> Users { get; set; }
    }
}
