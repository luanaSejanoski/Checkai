using Checkai.DTOs;
using Checkai.Services;
using Microsoft.AspNetCore.Mvc;

namespace Checkai.Controller;

[ApiController]
[Route("api/[controller]")]

public class HabitosLogController : ControllerBase
{
    private readonly HabitoLogService _habitoLogService;

    public HabitosLogController(HabitoLogService habitoLogService)
    {
        _habitoLogService = habitoLogService;
    }

    //marcar habito concluido
    [HttpPost]
    public IActionResult Criar(CriarHabitoLogDto dto)
    {
        var resultado = _habitoLogService.Criar(dto);

        if(resultado == null)
        {
            return BadRequest();
        }

        return Created("", resultado);
    }

    //listar sequencia de conclusao de habito
    [HttpGet("{habitoId}")]
    public IActionResult ListarPorHabito(int habitoId)
    {
        var resultado = _habitoLogService.ListarPorHabito(habitoId);

        if(resultado == null)
        {
            return NotFound();
        }

        return Ok(resultado);
    }

    //listar concluidos do dia
    [HttpGet]
    public IActionResult ListarConcluidosHoje()
    {
        var resultado = _habitoLogService.ListarConcluidosHoje();

        return Ok(resultado);
    }

    //desfazer concluido
    [HttpDelete("{habitoId}")]
    public IActionResult RemoverConcluido(int habitoId)
    {
        var resultado = _habitoLogService.RemoveConcluido(habitoId);

         if(resultado == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}