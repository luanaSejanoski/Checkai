using Checkai.DTOs;
using Checkai.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Checkai.Controller;

[ApiController]
[Route("api/[controller]")]

[Authorize]
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
        var usuarioId = int.Parse(User.FindFirst("UsuarioId")!.Value);

        var resultado = _habitoLogService.Criar(dto, usuarioId);

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
        var usuarioId = int.Parse(User.FindFirst("UsuarioId")!.Value);

        var resultado = _habitoLogService.ListarPorHabito(habitoId, usuarioId);

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
        var usuarioId = int.Parse(User.FindFirst("UsuarioId")!.Value);

        var resultado = _habitoLogService.ListarConcluidosHoje(usuarioId);

        return Ok(resultado);
    }

    //desfazer concluido
    [HttpDelete("{habitoId}")]
    public IActionResult RemoverConcluido(int habitoId)
    {
        var usuarioId = int.Parse(User.FindFirst("UsuarioId")!.Value);

        var resultado = _habitoLogService.RemoveConcluido(habitoId, usuarioId);

         if(resultado == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    //listar sequencia do habito
    [HttpGet("sequencia/{habitoId}")]
    public IActionResult CalcularSequencia(int habitoId)
    {
        var usuarioId = int.Parse(User.FindFirst("UsuarioId")!.Value);

        var resultado = _habitoLogService.CalcularSequencia(habitoId, usuarioId);

        return Ok(resultado);
    }

    //mostrar histórico do habito
    [HttpGet("historico/{habitoId}")]
    public IActionResult Historico( int habitoId)
    {
        Console.WriteLine("ENTROU NO CONTROLLER HISTORICO");
        var usuarioId = int.Parse(User.FindFirst("UsuarioId")!.Value);

        var resultado = _habitoLogService.Historico(habitoId, usuarioId);

        if(resultado == null)
        {
            return NotFound();
        }

        return Ok(resultado);
    }

    //mostrar dias restantes para concluir o habito
    [HttpGet("dias-restantes/{habitoId}")]
    public IActionResult CalcularDiasRestantes(int habitoId)
    {
        var usuarioId = int.Parse(User.FindFirst("UsuarioId")!.Value);

        var resultado = _habitoLogService.CalcularDiasRestantes(habitoId, usuarioId);

        if(resultado == null)
        {
            return NotFound();
        }

        return Ok(resultado);
    }

    //mostrar progresso do habito
    [HttpGet("progresso/{habitoId}")]
    public IActionResult CalcularProgresso(int habitoId)
    {
        var usuarioId = int.Parse(User.FindFirst("UsuarioId")!.Value);

        var resposta = _habitoLogService.CalcularProgresso(habitoId, usuarioId);

        if(resposta == null)
        {
            return NotFound();
        }

        return Ok(resposta);
    }

}