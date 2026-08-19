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
    public RespostaHabitoLogDto? Criar(CriarHabitoLogDto dto)
    {
        var habito = _habitoRepository.BuscarPorId(dto.HabitoId);

        if(habito == null)
        {
            return null;
        }

        //Verifica se o hábito já foi concluído hoje
        var logs =  _habitoLogRepository.ListarPorHabito(dto.HabitoId);

        var jaConcluidoHoje = logs.Any(l => l.Data.Date == DateTime.Now.Date);

        if(jaConcluidoHoje)
        {
            return null;
        }
        
        //Cria e salva um novo log
        var habitoLog = new HabitoLog
        {
            Data = DateTime.Now,
            Concluido = dto.Concluido,
            HabitoId = habito.Id
        
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

    //Listar Log por habito
    public List<RespostaHabitoLogDto>? ListarPorHabito(int habitoId){

        var habito = _habitoRepository.BuscarPorId(habitoId);

        if(habito == null)
        {
            return null;
        }

        var logs = _habitoLogRepository.ListarPorHabito(habitoId);

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
    public List<RespostaHabitoLogDto> ListarConcluidosHoje()
    {
        var logsConcluidos = _habitoLogRepository.ListarConcluidosHoje();

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
    public HabitoLog? RemoveConcluido(int habitoId)
    {
        var log = _habitoLogRepository.RemoveConluido(habitoId);

        if(log == null)
        {
            return null;
        }

        return log;
    }

    //Calcula a sequencia do habito
    public int CalcularSequencia(int habitoId)
    {
        var logs = _habitoLogRepository.ListarPorHabito(habitoId);
        
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
    public int? CalcularDiasRestantes(int habitoId)
    {
         var resultado = _habitoRepository.BuscarPorId(habitoId);

        if(resultado == null)
        {
            return null;
        }

        var sequenciaAtual = CalcularSequencia(habitoId);

        var diasRestantes = resultado.MetaDias - sequenciaAtual;

        if(diasRestantes < 0)
        {
            diasRestantes = 0;
        }
         
         return diasRestantes;
    }

      //Mostra historico do habito
      public List<RespostaHabitoLogDto>? Historico (int habitoId)
    {
        var resultado = _habitoRepository.BuscarPorId(habitoId);

        if(resultado == null)
        {
            return null;
        }

        var logs = _habitoLogRepository.ListarPorHabito(habitoId);
        
        var Historico = new List<RespostaHabitoLogDto>();

        var dataInicial = resultado.DataCriacao;

        for(int i = 0; i < resultado.MetaDias; i++)
        {
            var data = dataInicial.AddDays(i);

            var logDoDia = logs.FirstOrDefault(l => l.Data.Date == data);

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
    public ProgressoHabitoDto? CalcularProgresso(int habitoId)
    {
        var resultado = _habitoRepository.BuscarPorId(habitoId);

        if(resultado == null)
        {
            return null;
        }

        var sequenciaAtual = CalcularSequencia(habitoId);

        var diasRestantes = resultado.MetaDias - sequenciaAtual;

        if(diasRestantes < 0)
        {
            diasRestantes = 0;
        }

        var metaConcluida = sequenciaAtual >= resultado.MetaDias;

        var progresso = new ProgressoHabitoDto
        {
            NomeHabito = resultado.Nome,
            MetaDias = resultado.MetaDias,
            DiasRestantes = diasRestantes,
            Concluido = metaConcluida,
            SequenciaAtual = sequenciaAtual
        };

        return progresso;
}
}

