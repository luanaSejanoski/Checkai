// Service: responsável pelas regras de negócio e pela lógica do sistema.

using Checkai.Controllers;
using Checkai.DTOs;
using Checkai.Models;
using Checkai.Repositories;


namespace Checkai.Services;

public  class HabitoService
{
    private readonly HabitoRepository _repository;

    private readonly HabitoLogRepository _repositoryLog;

    private readonly HabitoLogService _habitoLogService;


    public HabitoService(HabitoRepository repository, HabitoLogRepository repositoryLog, HabitoLogService habitoLogService)
    {
        _repository = repository;
        _repositoryLog = repositoryLog;
        _habitoLogService = habitoLogService;

    }

    //criar habito
    public Habito? Criar(CriarHabitoDto dto, int usuarioId)
{
    if (string.IsNullOrWhiteSpace(dto.Nome))
    {
        throw new ArgumentException("O nome do hábito é obrigatório.");
    }

    if(dto.MetaDias < 1 || dto.MetaDias > 365)
        {
            return null;
        }

    var habito = new Habito
    {
        Nome = dto.Nome,
        Descricao = dto.Descricao,
        MetaDias = dto.MetaDias,
        DataCriacao = DateTime.Now.Date,
        UsuarioId = usuarioId,
        DataInicioMeta = DateTime.Now.Date
    };

    return _repository.Criar(habito);
}

    //listar habitos    
    public List<Habito> Listar(int usuarioId)
{
    var habitos = _repository.Listar(usuarioId);

        var habitosAtivos = new List<Habito>();

        foreach(var habito in habitos)
        {
            var resultado = _habitoLogService.CalcularSequencia(
                habito.Id,
                usuarioId
            );

            if(resultado < habito.MetaDias)
            {
                habitosAtivos.Add(habito);
            }
        }

        return habitosAtivos;
    }

    //buscar habito pelo id
    public Habito? BuscarPorId(int id, int usuarioId)
{
    return _repository.BuscarPorId(id, usuarioId);
}

    //alterar habito pelo id
    public Habito? Alterar (int id, CriarHabitoDto dto, int usuarioId)
{
    var habito = _repository.BuscarPorId(id, usuarioId);

        if(habito == null)
        {
            return null;
        }


        if(habito.MetaDias != dto.MetaDias)
        {
            habito.DataInicioMeta = DateTime.Now.Date;
            habito.DataAlteracaoMeta = DateTime.Now.Date;
        }

        habito.Nome = dto.Nome;
        habito.Descricao = dto.Descricao;
        habito.MetaDias = dto.MetaDias;

    return _repository.Alterar(habito);
}

    //remover habito pelo id
    public bool Remover (int id, int usuarioId)
{
    var habito = _repository.BuscarPorId(id, usuarioId);

     if(habito == null)
        {
            return false;
        }   

        _repository.Remover(habito);
        
    return true;
}

    //listar habitos concluidos
    public List<HabitoHojeDto> ListarHabitosConcluidos(int usuarioId)
    {
        var resposta = new List<HabitoHojeDto>();

        var habitos = _repository.Listar(usuarioId);

        var logsConcluidos = _repositoryLog.ListarConcluidosHoje(usuarioId);


        foreach(var habito in habitos)
        {
            var concluidoHoje = logsConcluidos.Any(h => h.HabitoId == habito.Id);

            var logs = _repositoryLog.ListarPorHabito(habito.Id, usuarioId);

            var logsConcluidoHabito = logs
            .Where(l => l.Concluido)
            .OrderByDescending(l => l.Data)
            .ToList();

            int sequencia = 0;

            if (logsConcluidoHabito.Any())
            {
                sequencia = 1;
            }

            for(int i = 1; i < logsConcluidoHabito.Count; i++)
            {
                if(logsConcluidoHabito[i].Data.Date == logsConcluidoHabito[i - 1].Data.Date.AddDays(-1))
                {
                    sequencia++;
                }
                else
                {
                    break;
                }
            }

            var metaConcluida = sequencia >= habito.MetaDias;

            if (metaConcluida)
            {
                continue;
            }

            var habitoHoje = new HabitoHojeDto
            {
                Id = habito.Id,
                Nome = habito.Nome,
                ConcluidoHoje = concluidoHoje
            };

            resposta.Add(habitoHoje);
        }
            return resposta;
    }

    //calcular maior sequencia entre todos os hábitos
    public int CalcularMaiorSequencia(int usuarioId)
    {
        var habitos = _repository.Listar(usuarioId);

        var maiorSequencia = 0;

        foreach(var habito in habitos)
        {
            var logs = _repositoryLog.ListarPorHabito(habito.Id, usuarioId);

            var logsConcluidos = logs.Where(l => l.Concluido)
             .OrderByDescending(l => l.Data)
             .ToList();

             if(logsConcluidos.Count == 0)
            {
                continue;
            }

            int sequencia = 1;

            for(int i = 1; i < logsConcluidos.Count; i++)
            {
                if(logsConcluidos[i].Data.Date == logsConcluidos[i - 1].Data.Date.AddDays(-1))
                {
                   sequencia++;
                }
                else
                {
                    // sequência acabou por causa de uma quebra
                    if(sequencia > maiorSequencia)
                    {
                        maiorSequencia = sequencia;
                    }

                    sequencia = 1;
                }
            }
            //sequência acabou porque chegamos ao final da lista.
            if(sequencia > maiorSequencia)
            {
                 maiorSequencia = sequencia;
            }

        }
            return maiorSequencia;
    }

    //listar todos os hábitos
    public List<Habito> ListarTodos(int usuarioId)
    {
        return _repository.Listar(usuarioId);
    }  
}