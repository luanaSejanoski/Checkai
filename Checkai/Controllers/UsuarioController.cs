using Checkai.DTOs;
using Checkai.Services;
using Microsoft.AspNetCore.Mvc;

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
}
