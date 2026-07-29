using Checkai.Services;
using Checkai.DTOs;
using Checkai.Models;
using Microsoft.AspNetCore.Mvc;

namespace Checkai.Controllers;

[ApiController]
[Route("api/[controller]")]
public  class HabitosController : ControllerBase
{
    private readonly HabitoService _habitoService;

    public HabitosController(HabitoService habitoService)
{
    _habitoService = habitoService;
}

    //post para criar os habitos
    [HttpPost]
    public IActionResult Criar(CriarHabitoDto dto)
    {
        var habito = _habitoService.Criar(dto);
       
        return Ok(habito);
    }

    //get para listar os habitos
    [HttpGet]
    public ActionResult<List<Habito>> Listar()
    {
        var habitos = _habitoService.Listar();

        return Ok(habitos);
    }

    //get para buscar habito pelo id
    [HttpGet("{id}")]
    public ActionResult<Habito> BuscarPorId (int id)
    {
        var habito = _habitoService.BuscarPorId(id);

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
        var habito = _habitoService.Alterar(id, dto);

        if(habito == null)
        {
            return NotFound();
        }

        return Ok(habito);
    }

    //delete para remover habito pelo id
    [HttpDelete("{id}")]
    public ActionResult Remover (int id)
    {
        var removido = _habitoService.Remover(id);

        if(!removido)
        {
            return NotFound();
        }

        return NoContent();
    }
}