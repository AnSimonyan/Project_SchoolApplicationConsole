using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Serilog.Core;
using Serilog;
using Serilog.Events;



namespace Projet1_ApplicationConsole
{
    internal class JsonFiles
    {

        public static void SaveJsonFile(UserTools userTool)
        {
            string fileNameStudents = ConstantsAPP.JSONFILENAME + "Scool.json";

            string json = JsonConvert.SerializeObject(userTool, Formatting.Indented);

            File.WriteAllText(fileNameStudents, json);

        }

        public static UserTools ReadJsonFile()
        {
            string fileNameStudents = ConstantsAPP.JSONFILENAME + "Scool.json";
            try
            {
                var jsonString = File.ReadAllText(fileNameStudents);
                return JsonConvert.DeserializeObject<UserTools>(jsonString);
            }
            catch
            {
                return null;
            }





        }
        


    }
}
