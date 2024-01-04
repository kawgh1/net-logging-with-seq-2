using net_logging_with_seq;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// SERILOG CONFIGURATION
// builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Information()
//    .WriteTo.Console()
//    .WriteTo.File("logs/mylogs-.txt", rollingInterval: RollingInterval.Day)
//    .CreateLogger();

var assemblyName = typeof(Program).Assembly.GetName().Name;
SerilogSeqConfigHelper.Configure(assemblyName);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();