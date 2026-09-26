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

    public Usuario? BuscarPorId(int id)
    {
        return _context.Usuario.FirstOrDefault(u => u.Id == id);
    }

    public Usuario Alterar(Usuario usuario)
    {
        _context.Usuario.Update(usuario);
        _context.SaveChanges();

        return usuario;
    }
}