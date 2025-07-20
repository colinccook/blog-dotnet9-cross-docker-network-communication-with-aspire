var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.AddServiceDefaults();

builder.Services.AddHttpClient("aspire-mockservice", httpClient =>
{
    httpClient.BaseAddress = new("http://_endpointname.servicename");
});

builder.Services.AddHttpClient("barservice.colin", httpClient =>
{
    httpClient.BaseAddress = new("http://barservice.colin");
});


app.MapGet("/via-container", (IHttpClientFactory httpClientFactory) =>
{
    var otherNetworkedContainerClient = httpClientFactory.CreateClient("aspire-mockservice");
    var result = otherNetworkedContainerClient.GetAsync("/foo");
    return result;
});

app.MapGet("/direct-request", (IHttpClientFactory httpClientFactory) =>
{
    var otherNetworkedContainerClient = httpClientFactory.CreateClient("barservice.colin");
    var result = otherNetworkedContainerClient.GetAsync("/bar");
    return result;
});

app.Run();
