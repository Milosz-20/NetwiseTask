using NetwiseTask.Services;

namespace NetwiseTask.Endpoints;

public static class FactEndpoint
{
    public static async Task<IResult> Handle(ICatFactService catFactService, IFactLogWriter logWriter)
    {
        try
        {
            var fact = await catFactService.GetRandomFactAsync();
            await logWriter.AppendAsync(fact);
            return Results.Ok(fact);
        }
        catch (HttpRequestException)
        {
            return Results.Problem(
                detail: "Nie udało się pobrać danych z catfact.ninja.",
                statusCode: StatusCodes.Status502BadGateway);
        }
    }
}
