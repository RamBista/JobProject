using JobBackEnd.DAL.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobBackEnd.DAL.Context;
public class JobDbContext : DbContext
{
    public JobDbContext(DbContextOptions<JobDbContext> options) : base(options) { }

    public DbSet<Candidate> Candidates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Email).IsRequired();
            entity.HasIndex(c => c.Email).IsUnique();
        });
    }
}