# .NET Logging with SEQ and Serilog

## Notes from Patrick
- https://www.youtube.com/watch?v=QjO2Jac1uQw

## Nuget Packages to install
- `Serilog.AspNetCore`
- `Serilog.Sinks.Seq`

## Program.cs
`
 builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));
`

## appsettings.json
`
 "Serilog": {
    "WriteTo": [
      {
        "Name": "Seq",
        "Args": {"serverUrl":  "http://localhost:5341"}
      }
    ]
  }
  `

## Seq Helper Class

`
     public static class SerilogSeqConfigHelper
    {
        public static void Configure(string applicationName)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
#if DEBUG
                 .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", $"{applicationName}")
                .WriteTo.File("logs/logs.txt")
                .WriteTo.Console()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();
        }
    }
    `
