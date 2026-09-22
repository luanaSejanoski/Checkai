using System.Text.Json.Serialization;


namespace Checkai.Models;

public class Habito
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public string Nome { get; set; } = string.Empty; //valor inicial é um texto vazio 

    public string Descricao { get; set; } = string.Empty;

    public int MetaDias { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime DataInicioMeta { get; set; }

    public DateTime DataAlteracaoMeta { get; set; }

    [JsonIgnore]
    public List<HabitoLog> Logs { get; set; } = new();
}