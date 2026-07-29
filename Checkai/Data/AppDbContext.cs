using Checkai.Models;
using Microsoft.EntityFrameworkCore;

namespace Checkai.Data;

public class AppDbContext : DbContext
{
    public DbSet<Habito> Habitos { get; set; }

    public DbSet<HabitoLog> HabitoLogs { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
}

