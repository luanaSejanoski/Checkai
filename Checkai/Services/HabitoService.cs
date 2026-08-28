// Service: responsável pelas regras de negócio e pela lógica do sistema.

using Checkai.DTOs;
using Checkai.Models;
using Checkai.Repositories;


namespace Checkai.Services;

public  class HabitoService
{
    private readonly HabitoRepository _repository;

    private readonly HabitoLogRepository _repositoryLog;

    public HabitoService(HabitoRepository repository, HabitoLogRepository repositoryLog)
    {
        _repository = repository;
        _repositoryLog = repositoryLog;
    }

    //criar habito
    public Habito? Criar(CriarHabitoDto dto)
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
        DataCriacao = DateTime.Now.Date
    };

    return _repository.Criar(habito);
}

    //listar habitos
    public List<Habito> Listar(int usuarioId)
{
    var habitos = _repository.Listar(usuarioId);

    return habitos;
}

    //buscar habito pelo id
    public Habito? BuscarPorId(int id)
{
    return _repository.BuscarPorId(id);
}

    //alterar habito pelo id
    public Habito? Alterar (int id, CriarHabitoDto dto)
{
    var habito = _repository.BuscarPorId(id);

        if(habito == null)
        {
            return null;
        }

        habito.Nome = dto.Nome;
        habito.Descricao = dto.Descricao;

    return _repository.Alterar(habito);
}

    //remover habito pelo id
    public bool Remover (int id)
{
    var habito = _repository.BuscarPorId(id);

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

        var logsConcluidos = _repositoryLog.ListarConcluidosHoje();

        foreach(var habito in habitos)
        {
            var concluidoHoje = logsConcluidos.Any(h => h.HabitoId == habito.Id);

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
}