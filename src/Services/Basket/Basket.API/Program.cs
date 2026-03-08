var builder = WebApplication.CreateBuilder(args);

//Add Services to the container

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//Configure the HTTP request pipeline

app.Run();
