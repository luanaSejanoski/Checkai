
namespace Checkai.DTOs;

public class RespostaHabitoLogDto
{
    public int Id { get; set;}

    public DateTime  Data { get; set; }

    public bool Concluido { get; set; }

    public int  HabitoId { get; set; }

    public string NomeHabito { get; set; } = string.Empty;

    public string? Mensagem { get; set; }
}