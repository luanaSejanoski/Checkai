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
}
