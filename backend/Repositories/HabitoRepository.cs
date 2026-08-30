// Repository: responsável pelo acesso e manipulação dos dados no banco.

using Checkai.Data;
using Checkai.Models;

namespace Checkai.Repositories;

public class HabitoRepository
{
    private readonly AppDbContext _context;

    public HabitoRepository(AppDbContext context)
    {
        _context = context;
    }

    public Habito Criar(Habito habito)
    {
        _context.Habitos.Add(habito);
        _context.SaveChanges();

        return habito;
    }

    public List<Habito> Listar(int usuarioId)
    {
        return _context.Habitos
          .Where(h => h.UsuarioId == usuarioId)
          .ToList();
    }

    public Habito? BuscarPorId(int id, int usuarioId)
    {
        return _context.Habitos.FirstOrDefault(h => h.Id == id && h.UsuarioId == usuarioId);
    }

    public Habito? Alterar(Habito habito)
    {
        _context.SaveChanges();

        return habito;
    }

    public void Remover(Habito habito)
    {
        _context.Habitos.Remove(habito);
        _context.SaveChanges();
    }
}