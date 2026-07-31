using Checkai.Data;
using Checkai.Migrations;
using Checkai.Models;

namespace Checkai.Repositories;

public class HabitoLogRepository
{
    private readonly AppDbContext _context;

    public HabitoLogRepository (AppDbContext context)
    {
        _context = context;
    }

    public HabitoLog Criar (HabitoLog habitoLog)
    {
        _context.HabitoLogs.Add(habitoLog);
        _context.SaveChanges();

        return habitoLog;
    }

    public List<HabitoLog> ListarPorHabito(int habitoId)
    {
         var logs = _context.HabitoLogs
         .Where(h => h.HabitoId == habitoId)
         .ToList();

         return logs;
    }
}


