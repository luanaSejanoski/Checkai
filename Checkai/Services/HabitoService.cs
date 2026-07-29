using Checkai.DTOs;
using Checkai.Models;
using Checkai.Repositories;

namespace Checkai.Services;

public  class HabitoService
{
    private readonly HabitoRepository _repository;

    public HabitoService(HabitoRepository repository)
    {
        _repository = repository;
    }

    //criar habito
public Habito Criar(CriarHabitoDto dto)
{
    if (string.IsNullOrWhiteSpace(dto.Nome))
    {
        throw new ArgumentException("O nome do hábito é obrigatório.");
    }

    var habito = new Habito
    {
        Nome = dto.Nome,
        Descricao = dto.Descricao
    };

    return _repository.Criar(habito);
}

    //listar habitos
    public List<Habito> Listar()
{
    return _repository.Listar();
}

    //buscra habito por id
    public Habito? BuscarPorId(int id)
{
    return _repository.BuscarPorId(id);
}

    //alterar habito
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

    //remover habito
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
}