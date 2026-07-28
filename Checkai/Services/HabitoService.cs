using System.Diagnostics;
using Checkai.Data;
using Checkai.DTOs;
using Checkai.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Checkai.Services;

public  class HabitoService
{
    private readonly AppDbContext _context;

    public HabitoService(AppDbContext context)
    {
        _context = context;
    }

    public Habito Criar(CriarHabitoDto dto)
    {
        var habito = new Habito
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao
        };

        _context.Habitos.Add(habito);
        _context.SaveChanges();

        return habito;
    }

    public List<Habito> Listar()
    {
        return _context.Habitos.ToList();
    }

    public Habito? BuscarPorId(int id)
    {
        return _context.Habitos.FirstOrDefault(h => h.Id == id);
    }

    public Habito? Alterar (int id, CriarHabitoDto dto)
    {
        var habito = _context.Habitos.FirstOrDefault(h => h.Id == id);

        if(habito == null)
        {
            return null;
        }

        habito.Nome = dto.Nome;
        habito.Descricao = dto.Descricao;

        _context.SaveChanges();

        return habito;
    }

    public bool Remover (int id)
    {
     var habito = _context.Habitos.FirstOrDefault(h => h.Id == id);

     if(habito == null)
        {
            return false;
        }   

        _context.Habitos.Remove(habito);
        _context.SaveChanges();

        return true;
    }
}