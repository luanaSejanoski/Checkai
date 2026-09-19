// Service: responsável pelas regras de negócio e pela lógica do sistema.
using System.Data;
using Checkai.DTOs;
using Checkai.Models;
using Checkai.Repositories;

namespace Checkai.Services;

public class HabitoLogService
{
    private readonly HabitoLogRepository _habitoLogRepository;
    private readonly HabitoRepository _habitoRepository;

    public HabitoLogService(HabitoLogRepository habitoLogRepository, HabitoRepository habitoRepository)
    {
        _habitoLogRepository = habitoLogRepository;
        _habitoRepository = habitoRepository;
    }

    //Criar conclusão do hábito
    public RespostaHabitoLogDto? Criar(CriarHabitoLogDto dto, int usuarioId)
    {
        var habito = _habitoRepository.BuscarPorId(dto.HabitoId, usuarioId);

        if(habito == null)
        {
            return null;
        }

        var sequenciaAtual = CalcularSequencia(dto.HabitoId, usuarioId);

        var metaConcluida = sequenciaAtual >= habito.MetaDias;

        if (metaConcluida)
        {
            return null;
        }

        //Verifica se o hábito já foi concluído hoje
        var logs =  _habitoLogRepository.ListarPorHabito(dto.HabitoId, usuarioId);

        var logHoje = logs.FirstOrDefault(l => l.Data.Date == DateTime.Now.Date);
  
        if(logHoje == null)
        {

        //Cria e salva um novo log
        var habitoLog = new HabitoLog
        {
            Data = DateTime.Now,
            Concluido = dto.Concluido,
            HabitoId = habito.Id,
            UsuarioId = usuarioId
        };

        var logCriado = _habitoLogRepository.Criar(habitoLog);

            var resposta = new RespostaHabitoLogDto
            {
                Id = logCriado.Id,
                Data = logCriado.Data,
                Concluido = logCriado.Concluido,
                HabitoId = logCriado.HabitoId

            };
        
        return resposta;
        }

        if(dto.Concluido == false)
        {
            var log = _habitoLogRepository.RemoveConcluido(dto.HabitoId, usuarioId);

            if (log == null)
            {
                return null;
            }

            var resposta = new RespostaHabitoLogDto
            {
                Id = log.Id,
                Data = log.Data,
                Concluido = log.Concluido,
                HabitoId = log.HabitoId
            };

            return resposta;
        }
        return null;
    }

    //Listar Log por habito
    public List<RespostaHabitoLogDto>? ListarPorHabito(int habitoId, int usuarioId){

        var habito = _habitoRepository.BuscarPorId(habitoId, usuarioId);

        if(habito == null)
        {
            return null;
        }

        var logs = _habitoLogRepository.ListarPorHabito(habitoId, usuarioId);

        var resposta = new List<RespostaHabitoLogDto>();

        foreach(var log in logs)
        {
            var dto = new RespostaHabitoLogDto
            {
                Id = log.Id,
                Data = log.Data,
                Concluido = log.Concluido,
                HabitoId = log.HabitoId,
                NomeHabito =  log.Habito.Nome
            };

            resposta.Add(dto);
        }
        return resposta;
    }

    //Listar logs concluidos do dia
    public List<RespostaHabitoLogDto> ListarConcluidosHoje(int usuarioId)
    {
        var logsConcluidos = _habitoLogRepository.ListarConcluidosHoje(usuarioId);

        var resposta = new List<RespostaHabitoLogDto>();

        foreach(var log in logsConcluidos)
        {
            var dto = new RespostaHabitoLogDto
            {
                Id = log.Id,
                Data = log.Data,
                Concluido = log.Concluido,
                HabitoId = log.HabitoId,
                NomeHabito = log.Habito.Nome
            };

            resposta.Add(dto);
        }
        
        return resposta;
    }

    //Desmarca conclusão do habito
    public HabitoLog? RemoveConcluido(int habitoId, int usuarioId)
    {
        var habito = _habitoRepository.BuscarPorId(habitoId, usuarioId);

        if(habito == null)
        {
            return null;
        }

        var log = _habitoLogRepository.RemoveConcluido(habitoId, usuarioId);

        if(log == null)
        {
            return null;
        }

        return log;
    }

    //Calcula a sequencia do habito
    public int CalcularSequencia(int habitoId, int usuarioId)
    {
          var habito = _habitoRepository.BuscarPorId(habitoId, usuarioId);

            if(habito == null)
            {
                return 0;
            }

        var logs = _habitoLogRepository.ListarPorHabito(habitoId, usuarioId);
        
        var logsConcluidos = logs.Where(l => l.Concluido)
        .OrderByDescending(l => l.Data)
        .ToList();

        if(logsConcluidos.Count == 0)
        {
            return 0;
        }

        //pega o primeiro log da lista e compara com a data atual
        var concluidoHoje = logsConcluidos[0].Data.Date == DateTime.Now.Date;

        if (!concluidoHoje)
        {
            return 0;
        }

        int sequencia = 1;

        for(int i = 1; i < logsConcluidos.Count; i++)
        {
            // Verifica se as datas são consecutivas e interrompe a contagem ao encontrar uma falha.
            if(logsConcluidos[i].Data.Date == logsConcluidos[i - 1].Data.Date.AddDays(-1))
            {
                sequencia++;
            }
            else
            {
                break;
            }
        }
            return sequencia;
    }

    //Calcula dias restantes para concluir o habito
    public int? CalcularDiasRestantes(int habitoId, int usuarioId)
    {
         var resultado = _habitoRepository.BuscarPorId(habitoId, usuarioId);

        if(resultado == null)
        {
            return null;
        }

        var sequenciaAtual = CalcularSequencia(habitoId, usuarioId);

        var diasRestantes = resultado.MetaDias - sequenciaAtual;

        if(diasRestantes < 0)
        {
            diasRestantes = 0;
        }
         
         return diasRestantes;
    }

      //Mostra historico do habito
      public List<RespostaHabitoLogDto>? Historico (int habitoId, int usuarioId)
      
    {
        var resultado = _habitoRepository.BuscarPorId(habitoId, usuarioId);

        if(resultado == null)
        {
            return null;
        }

        var logs = _habitoLogRepository.ListarPorHabito(habitoId, usuarioId);
        
        var Historico = new List<RespostaHabitoLogDto>();

        var dataInicial = resultado.DataCriacao;

        var dataFinal = DateTime.Today;


        for(int i = 0; dataInicial.AddDays(i) <= dataFinal; i++)
        {
            var data = dataInicial.AddDays(i);

            var logDoDia = logs.FirstOrDefault(l => l.Data.Date == data.Date);

            RespostaHabitoLogDto resposta;

            if(logDoDia == null)
            {
                resposta = new RespostaHabitoLogDto
                {
                    Data = data,
                    Concluido = false,
                    HabitoId = resultado.Id,
                    NomeHabito = resultado.Nome
                };
            }
           else
{
                resposta = new RespostaHabitoLogDto
            {
                Id = logDoDia.Id,
                Data = logDoDia.Data,
                Concluido = logDoDia.Concluido,
                HabitoId = logDoDia.HabitoId,
                NomeHabito = resultado.Nome
            };
}

             Historico.Add(resposta);
    } 

        return Historico;
}

    //Calcula progresso do habito
    public ProgressoHabitoDto? CalcularProgresso(int habitoId, int usuarioId)
    {
        var resultado = _habitoRepository.BuscarPorId(habitoId, usuarioId);

        if(resultado == null)
        {
            return null;
        }

        var sequenciaAtual = CalcularSequencia(habitoId, usuarioId);

        var diasRestantes = resultado.MetaDias - sequenciaAtual;

        if(diasRestantes < 0)
        {
            diasRestantes = 0;
        }

        var metaConcluida = sequenciaAtual >= resultado.MetaDias;

        string? mensagem = null;

        if (metaConcluida)
        {
            mensagem = $"🎉 Meta de {resultado.MetaDias} dias concluída!";
        }

        var progresso = new ProgressoHabitoDto
        {
            NomeHabito = resultado.Nome,
            MetaDias = resultado.MetaDias,
            DiasRestantes = diasRestantes,
            Concluido = metaConcluida,
            SequenciaAtual = sequenciaAtual,
            Mensagem = mensagem
        };

        return progresso;
}
}

