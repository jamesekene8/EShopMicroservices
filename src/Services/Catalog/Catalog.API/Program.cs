using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the container
builder.Services.AddCarter();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

//Configure the HTTP request pipeline
app.MapCarter();


app.Run();
