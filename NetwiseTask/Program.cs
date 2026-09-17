using NetwiseTask.Endpoints;
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

app.MapGet("/fact", FactEndpoint.Handle)
    .WithName("GetCatFact");

app.Run();