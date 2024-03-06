using Projects;

var builder = DistributedApplication.CreateBuilder(args);

//builder.AddProject<Projects.eShopAPI>("eshopAPI");

builder.Build().Run();
