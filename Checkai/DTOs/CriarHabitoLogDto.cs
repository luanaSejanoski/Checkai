using System.ComponentModel.DataAnnotations;

namespace Checkai.DTOs;

public class CriarHabitoLogDto
{ 
    [Range(1, int.MaxValue)]
    public int HabitoId { get; set; }

    public bool Concluido { get; set; }
}