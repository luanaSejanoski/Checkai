// Repository: responsável pelo acesso e manipulação dos dados no banco.

using Checkai.Data;
using Checkai.Models;
using Microsoft.EntityFrameworkCore;

namespace Checkai.Repositories;

public class HabitoLogRepository
{
    private readonly AppDbContext _context;
    

    public HabitoLogRepository (AppDbContext context)
    {
        _context = context;
    }
   
    //Criar habito
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
        .Include(h => h.Habito)
        .ToList();

         return logs;
    }

    public List<HabitoLog> ListarConcluidosHoje(int usuarioId)
    {
        var logs = _context.HabitoLogs
        .Where(h => h.Data.Date == DateTime.Now.Date && h.Concluido && h.UsuarioId == usuarioId)
        .Include(h => h.Habito)
        .ToList();

        return logs;
    }

    public HabitoLog? RemoveConcluido(int habitoId)
    {
        var log = _context.HabitoLogs
        .Where(h => h.HabitoId == habitoId &&
                h.Data.Date == DateTime.Now.Date &&
                h.Concluido)
        .FirstOrDefault();

        if(log == null)
        {
            return null;
        }
        
       _context.HabitoLogs.Remove(log);
       _context.SaveChanges();

       return log;

    }
}


