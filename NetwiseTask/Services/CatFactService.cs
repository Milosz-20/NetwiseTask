using NetwiseTask.Models;

namespace NetwiseTask.Services;

public class CatFactService : ICatFactService
{
    private readonly HttpClient _httpClient;

    public CatFactService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CatFact> GetRandomFactAsync()
    {
        var fact = await _httpClient.GetFromJsonAsync<CatFact>("fact");
        return fact ?? throw new InvalidOperationException("API zwróciło pustą odpowiedź :(");
    }
}

