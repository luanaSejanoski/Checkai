using Checkai.DTOs;
using Checkai.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Checkai.Controller;

[ApiController]
[Route("api/[controller]")]

public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _userService;

    public UsuarioController(UsuarioService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public IActionResult Criar(CriarUsuarioDto dto)
    {
        _userService.Criar(dto);

        return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login(LoginUsuarioDto dto)
    {
        var resultado = _userService.Login(dto);

        if (resultado == null)
        {
            return BadRequest("Usuário ou senha Inválidos!!");
        }

        return Ok(new
        {
            mensagem = "Login realizado com sucesso!!",
            token = resultado
        }
        );
    }

    [Authorize]
    [HttpGet("perfil")]
    public IActionResult Perfil()
    {
        var usuarioId = User.FindFirst("UsuarioId");

        var id = int.Parse(usuarioId.Value);

        var usuario = _userService.BuscarPorId(id);

        if(usuario == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        return Ok(usuario);
    }
    
}
