
using Serilog.Events;
using Serilog;
using System.Globalization;


namespace Projet1_ApplicationConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File(ConstantsAPP.LOGPATH, rollingInterval: RollingInterval.Day)  // Log to a file
            .CreateLogger();

            Log.Information("Start");
            
            AppData appToolsMain = new AppData();
         
            DatabaseTools dataInitised = new DatabaseTools();
            
            appToolsMain = dataInitised.InitialiseData();

            Menu menu = new Menu(true, appToolsMain);

        }
    }
}

