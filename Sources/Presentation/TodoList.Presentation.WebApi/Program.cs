using TodoList.Core.Application.Ioc;
using TodoList.Infra.Persistence.Ioc;
using TodoList.Presentation.WebApi.Extensions;

// Configure services
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceLayer();
builder.Services.AddApplicationLayer();
builder.Services.AddControllerExtension();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExtension();
builder.Services.AddNotificationContextExtension();
builder.Services.AddCorrelationIdContextExtension();
builder.Services.AddApiVersioningExtension();

// Configure logs
builder.Logging.AddFile("logs/todo-app.json", isJson: true);

// Configure app
var app = builder.Build();

app.UseCorrelationIdHandleExtensions();
//app.UseDeveloperExceptionPage();

app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseErrorHandlingExtension();
app.UseHttpRequestBodyLoggerExtension();
app.UseSwaggerExtension();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();
app.Run();