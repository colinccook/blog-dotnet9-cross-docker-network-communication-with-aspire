var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHttpClient("aspire-mockservice", httpClient =>
{
    httpClient.BaseAddress = new("http://_endpointname.servicename");
});

builder.Services.AddHttpClient("barservice.colin", httpClient =>
{
    httpClient.BaseAddress = new("http://barservice.colin");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/via-container", async (IHttpClientFactory httpClientFactory) =>
{
    var client = httpClientFactory.CreateClient("aspire-mockservice");
    var result = await client.GetAsync("/foo");
    return Results.StatusCode((int)result.StatusCode);
});

app.MapGet("/direct-request", async (IHttpClientFactory httpClientFactory) =>
{
    var client = httpClientFactory.CreateClient("barservice.colin");
    var result = await client.GetAsync("/bar");
    return Results.StatusCode((int)result.StatusCode);
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
