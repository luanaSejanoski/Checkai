using Checkai.DTOs;
using Checkai.Models;
using Checkai.Repositories;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt; //transforma essas informações em um JWT
using System.Security.Claims; //informações do usuário

namespace Checkai.Services;

public class UsuarioService
{
    private readonly UsuarioRepository _userRepository;
    private readonly IConfiguration _configuration;
    
public UsuarioService(UsuarioRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration =  configuration;
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

public string? Login(LoginUsuarioDto dto)
    {
        var usuario = _userRepository.BuscarPorEmail(dto.Email);

        if(usuario == null)
        {
            return null;
        }

        if(!BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))// se a senha nao for valida
        {
            return null;
        }

        var jwtSettings = _configuration.GetSection("Jwt");

        var claims = new[]
        {
            new Claim("UsuarioId", usuario.Id.ToString()),
            new Claim("Email", usuario.Email)
        };

        var chave = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
);
        var credenciais = new SigningCredentials(chave,
        SecurityAlgorithms.HmacSha256
);
        var token = new JwtSecurityToken(
        issuer: jwtSettings["Issuer"],
        audience: jwtSettings["Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(15),
        signingCredentials: credenciais
);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;

    }

    public RespostaUsuarioDto? BuscarPorId(int id)
    {
       var user = _userRepository.BuscarPorId(id);

       return user;
    }
}
