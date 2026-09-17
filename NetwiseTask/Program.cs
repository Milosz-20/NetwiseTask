using NetwiseTask.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddHttpClient<ICatFactService, CatFactService>(client =>
{
    client.BaseAddress = new Uri("https://catfact.ninja/");
});

builder.Services.AddSingleton<IFactLogWriter, FactLogWriter>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
};

app.UseHttpsRedirection();

app.MapGet("/fact", async (ICatFactService catFactService, IFactLogWriter logWriter) =>
{
    var fact = await catFactService.GetRandomFactAsync();
    await logWriter.AppendAsync(fact);
    return fact;
})
.WithName("GetCatFact");

app.Run();