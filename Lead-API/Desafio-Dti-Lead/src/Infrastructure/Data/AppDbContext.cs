using Desafio_Dti_Lead.Domian.Entities;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Dti_Lead.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Lead> leads { get; set; }
}