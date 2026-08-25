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

public bool Login(LoginUsuarioDto dto)
    {
        var usuario = _userRepository.BuscarPorEmail(dto.Email);

        if(usuario == null)
        {
            return false;
        }

        if(!BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))// se a senha nao for valida
        {
            return false;
        }

        return true;

    }
}
