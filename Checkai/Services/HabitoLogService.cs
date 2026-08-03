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
}

