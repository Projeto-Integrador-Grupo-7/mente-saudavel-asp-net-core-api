using MenteSaudavelAPI;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
Startup.ConfigureServices(builder);

WebApplication app = builder.Build();
Startup.ConfigureApplication(app);

app.Run();