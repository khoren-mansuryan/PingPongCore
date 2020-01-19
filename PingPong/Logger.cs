using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPong
{
    public static class Logger
    {
        public static readonly ILogger FileLogger;
        public static readonly ILogger Conosle;
        public static string Path { get; set; }

        static Logger()
        {
            FileLogger = new LoggerConfiguration()
                 .MinimumLevel.Debug()
                 .WriteTo.File(Path + "\\consoleapp.log", outputTemplate:
        "{Message:lj}{NewLine}{Exception}")
                 .CreateLogger();

            Conosle = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole(outputTemplate:
        "{Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}
