using MiddlewaresExample.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddLogging();
var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddConsole() // Konsol loglamasý ekleyin
        .AddDebug();  // Hata ayýklama loglamasý ekleyin
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<RequestResponseMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
