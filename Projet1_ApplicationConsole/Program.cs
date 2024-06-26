
using Serilog.Events;
using Serilog;
using System.Globalization;




namespace Projet1_ApplicationConsole
{
    internal class Program
    {
        public static List<Student> StudentsList = new List<Student>();
        public static List<Course> CoursesList = new List<Course>();
        public static int MaxIDStudent; public static int MaxIDCours;

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File(ConstantsAPP.LOGPATH, rollingInterval: RollingInterval.Day)  // Log to a file
            .CreateLogger();


            Log.Information("Start");
            UserTools deserializedUserTools = JsonFiles.ReadJsonFile();

            if (deserializedUserTools != null)
            {
                StudentsList = deserializedUserTools.StudentsList;

                CoursesList = deserializedUserTools.CoursesList;
            }
            Menu menu = new Menu(true, StudentsList, CoursesList);
            

        }


    }


}

