using Checkai.DTOs;
using Checkai.Models;
using Checkai.Repositories;

namespace Checkai.Services;

public class HabitoLogService
{
    private readonly HabitoLogRepository _repository;

    public HabitoLogService(HabitoLogRepository repository)
    {
        _repository = repository;
    }
}

//criei o CriarHabitoLogDto, HabitoLogService, HabitoLog.cs