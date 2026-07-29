using Checkai.Data;
using Checkai.Models;

namespace Checkai.Repositories;

public class HabitoLogRepository
{
    private readonly AppDbContext _context;

    public HabitoLogRepository (AppDbContext context)
    {
        _context = context;
    }
}
