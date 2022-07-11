using TodoList.Core.Application.Ioc;
using TodoList.Infra.Persistence.Ioc;
using TodoList.Presentation.WebApi.Extensions;

// Configure services

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceLayer();
builder.Services.AddApplicationLayer();
builder.Services.AddControllerExtension();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseSwaggerExtension();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();
app.Run();