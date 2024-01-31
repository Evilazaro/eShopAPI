var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.eShopAPI>("eShop-API");

builder.Build().Run();
