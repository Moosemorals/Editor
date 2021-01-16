using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Serilog;
using Serilog.Events;

namespace Editor {
    public class Program {
        public static int Main(string[] args) {

            string outputTemplate = "[{Timestamp:HH:mm:ss} {Level}] {SourceContext:l} {Message}{Exception}{NewLine}";


            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Console(outputTemplate: outputTemplate)
                .CreateLogger();

            try {
                CreateHostBuilder(args).Build().Run();

                return 0; // Success
            } catch (Exception ex) {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1; // Failure
            } finally {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
