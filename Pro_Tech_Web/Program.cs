using Pro_Tech_Web;
using Pro_Tech_Web.Limit;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
var configuration = builder.Configuration;
var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestLimit>(configuration);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
