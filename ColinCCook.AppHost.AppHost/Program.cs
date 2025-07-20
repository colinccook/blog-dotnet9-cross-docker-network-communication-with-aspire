var builder = DistributedApplication.CreateBuilder(args);

var service = builder
    .AddContainer("servicename", "mockserver/mockserver")
    .WithHttpEndpoint(port: 1080, targetPort: 1080, "endpointname")
    .WithBindMount("./config", "/config/", isReadOnly: true)
    .WithEnvironment("MOCKSERVER_INITIALIZATION_JSON_PATH", "/config/expectation.json")
    .WithContainerRuntimeArgs("--add-host", "barservice.colin:host-gateway");;

var aspireRunService = builder
    .AddProject<Projects.ColinCCook_AspireRunService>("aspirerunservice")
    .WithReference(service.GetEndpoint("endpointname"));

builder.Build().Run();
