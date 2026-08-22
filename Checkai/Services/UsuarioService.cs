using Checkai.DTOs;
using Checkai.Models;
using Checkai.Repositories;

namespace Checkai.Services;

public class UsuarioService
{
    private readonly UsuarioRepository _userRepository;
    
public UsuarioService(UsuarioRepository userRepository)
    {
        _userRepository = userRepository;
    }

public void Criar(CriarUsuarioDto dto)
    {
        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            // Transforma a senha em um hash seguro antes de armazená-la no banco
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
        };

        _userRepository.Criar(usuario);
}
}
