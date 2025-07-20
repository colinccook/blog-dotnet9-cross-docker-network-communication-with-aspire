namespace ColinCCook.AppHost.AppHost.IntegrationTests;

public class AspireRunServiceTests
{
    [Test]
    public async Task CallingViaDirectRequest_Should_ReturnAccepted()
    {
        // Arrange
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.ColinCCook_AppHost_AppHost>();
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });
        await using var app = await appHost.BuildAsync();
        var resourceNotificationService = app.Services.GetRequiredService<ResourceNotificationService>();
        await app.StartAsync();

        // Act
        var httpClient = app.CreateHttpClient("aspirerunservice");
        await resourceNotificationService.WaitForResourceAsync("aspirerunservice", KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));
        var response = await httpClient.GetAsync("/direct-request");

        // Assert
        // If this fails, make sure you're running the docker-compose first and edit your hosts file as per the README.md
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
    }

    [Test]
    public async Task CallingViaContainer_Should_ReturnCreated()
    {
        // Arrange
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.ColinCCook_AppHost_AppHost>();
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });
        await using var app = await appHost.BuildAsync();
        var resourceNotificationService = app.Services.GetRequiredService<ResourceNotificationService>();
        await app.StartAsync();

        // Act
        var httpClient = app.CreateHttpClient("aspirerunservice");
        await resourceNotificationService.WaitForResourceAsync("aspirerunservice", KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));
        var response = await httpClient.GetAsync("/via-container");

        // Assert
        // If this fails, make sure you're running the docker-compose first and edit your hosts file as per the README.md
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
    }
}
