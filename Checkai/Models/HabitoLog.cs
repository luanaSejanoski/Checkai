namespace Checkai.Models;

public class HabitoLog
{
    public int Id { get; set; }

    public DateTime Data { get; set; }

    public bool Concluido { get; set; }

    //Foreign key
    public int HabitoId { get; set; }

    //Navegação
    public Habito Habito { get; set; }
}