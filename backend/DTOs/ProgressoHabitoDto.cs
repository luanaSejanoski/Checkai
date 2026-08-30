namespace Checkai.DTOs;

public class ProgressoHabitoDto
{
   public string NomeHabito { get; set; }
   
   public int MetaDias { get; set; }

   public int SequenciaAtual { get; set; }

   public int DiasRestantes { get; set; }

   public bool Concluido { get; set; }

   public string? Mensagem { get; set; }

}