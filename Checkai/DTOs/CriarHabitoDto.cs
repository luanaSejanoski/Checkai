using System.ComponentModel.DataAnnotations;

namespace Checkai.DTOs;

public class CriarHabitoDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Nome {get; set;} = string.Empty;

    [Required]
    [MinLength(5)]
    [MaxLength(300)]
    public string Descricao {get; set;} = string.Empty;
}