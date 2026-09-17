using System.Text.Json;
using NetwiseTask.Models;

namespace NetwiseTask.Services;

public class FactLogWriter : IFactLogWriter
{
    private readonly string _filePath;
    private readonly SemaphoreSlim _writeLock = new(1, 1);

    public FactLogWriter(IHostEnvironment env)
    {
        _filePath = Path.Combine(env.ContentRootPath, "catfacts.txt");
    }

    public async Task AppendAsync(CatFact fact)
    {
        var line = JsonSerializer.Serialize(fact);

        await _writeLock.WaitAsync();
        try
        {
            await File.AppendAllTextAsync(_filePath, line + Environment.NewLine);
        }
        finally
        {
            _writeLock.Release();
        }
    }
}

