using NetwiseTask.Models;

namespace NetwiseTask.Services
{
    public interface ICatFactService
    {
        Task<CatFact> GetRandomFactAsync();
    }
}
