
using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container.

builder.Services.AddCarter();

builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    });

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);  // Validtion DI

builder.Services.AddMarten(config => 
    {
        config.Connection(builder.Configuration.GetConnectionString("Database")!);        
    }).UseLightweightSessions();

var app = builder.Build();

//Configure the HTTP request pipeline.

app.MapCarter();

app.Run();
