using Checkai.Data;
using Checkai.DTOs;
using Checkai.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Checkai.Controllers;

[ApiController]
[Route("api/[controller]")]
public  class HabitosController : ControllerBase
{
    private readonly AppDbContext _context;

    public HabitosController(AppDbContext context)
    {
        _context = context;
    }

    //post para criar os habitos
    [HttpPost]
    public IActionResult Criar(CriarHabitoDto dto)
    {
        var habito = new Habito
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao
        };

        _context.Habitos.Add(habito);
        _context.SaveChanges();

        return Ok();
    }

    //get para listar os habitos
    [HttpGet]
    public ActionResult<List<Habito>> Listar()
    {
        var habitos = _context.Habitos.ToList();

        return Ok(habitos);
    }

    //get para buscar habito pelo id
    [HttpGet("{id}")]
    public ActionResult<Habito> BuscarPorId (int id)
    {
        var habito = _context.Habitos.FirstOrDefault(h => h.Id == id);

        if(habito == null)
        {
            return NotFound();
        }

        return Ok(habito);
    }

    //put para alterar habito pelo id
    [HttpPut("{id}")]
    public ActionResult<Habito> AlterarPorId (int id, CriarHabitoDto dto)
    {
        var habito = _context.Habitos.FirstOrDefault(h => h.Id == id);

        if(habito == null)
        {
            return NotFound();
        }

        habito.Nome = dto.Nome;
        habito.Descricao = dto.Descricao;

        _context.SaveChanges();

        return Ok(habito);

    }

    //delete para remover habito pelo id
    [HttpDelete("{id}")]
    public ActionResult Remover (int id)
    {
        var habito = _context.Habitos.FirstOrDefault(h => h.Id == id);

        if(habito == null)
        {
            return NotFound();
        }

        _context.Habitos.Remove(habito);
        _context.SaveChanges();

        return NoContent();
    }
}