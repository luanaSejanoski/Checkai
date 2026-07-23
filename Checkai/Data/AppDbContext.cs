using Checkai.Models;
using Microsoft.EntityFrameworkCore;

namespace Checkai.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Habito> Habitos {get; set;}
}

