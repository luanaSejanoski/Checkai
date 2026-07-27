
using Checkai.Data;
using Checkai.DTOs;
using Checkai.Models;
using Microsoft.AspNetCore.Mvc;

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
}