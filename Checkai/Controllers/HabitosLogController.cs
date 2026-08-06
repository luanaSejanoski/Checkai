using Checkai.DTOs;
using Checkai.Models;
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

    [HttpPost]
    public IActionResult Criar(CriarHabitoLogDto dto)
    {
        var resultado = _habitoLogService.Criar(dto);

        if(resultado == null)
        {
            return BadRequest();
        }

        return Ok(resultado);
    }

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
}