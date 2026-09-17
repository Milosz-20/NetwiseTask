using NetwiseTask.Models;

namespace NetwiseTask.Services;

public interface IFactLogWriter
{
    Task AppendAsync(CatFact fact);
}

