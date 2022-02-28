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
            modelBuilder.Entity<Usuarios>(eb =>
            {
                eb.HasKey(c => new {c.Id);
            });
        }

        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
