using System.Data;
using System.Xml;
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

    //Criar log de conclusão do hábito
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
                HabitoId = log.HabitoId
            };

            resposta.Add(dto);
        }
        return resposta;
    }

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

    public HabitoLog? RemoveConcluido(int habitoId)
    {
        var log = _habitoLogRepository.RemoveConluido(habitoId);

        if(log == null)
        {
            return null;
    
        }
        
        return log;
    }

}

