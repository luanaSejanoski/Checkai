using Checkai.Data;
using Checkai.Models;
using Checkai.DTOs;

namespace Checkai.Repositories;

public class UsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Criar(Usuario usuario)
    {
        _context.Usuario.Add(usuario);
        _context.SaveChanges();
    }

    public Usuario? BuscarPorEmail(string email)
    {
        return _context.Usuario.FirstOrDefault(u => u.Email == email);
    }

    public RespostaUsuarioDto? BuscarPorId(int id)
    {
        var user = _context.Usuario.FirstOrDefault(u => u.Id == id);

        if(user == null)
        {
            return null;
        }

        var resposta = new RespostaUsuarioDto
        {
            Nome = user.Nome,
            Email = user.Email
        };

        return resposta;
    }
}