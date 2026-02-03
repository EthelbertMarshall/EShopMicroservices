
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.


WebApplication app = builder.Build();

app.MapGet("/", () => "Hello World!");

//Configure the HTTP request pipeline.

app.Run();
