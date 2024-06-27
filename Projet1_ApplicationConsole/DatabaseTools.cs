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
    public  class DatabaseTools : IDatabaseTools
    {       
        public  void SaveData(AppData inisialisedAppData)
        {
            string fileNameStudents = ConstantsAPP.JSONFILENAME + "Scool.json";

            string json = JsonConvert.SerializeObject(inisialisedAppData, Formatting.Indented);

            File.WriteAllText(fileNameStudents, json);

        }

        public  AppData InitialiseData()
        {
            string fileNameStudents = ConstantsAPP.JSONFILENAME + "Scool.json";
            try
            {
                var jsonString = File.ReadAllText(fileNameStudents);
                return JsonConvert.DeserializeObject<AppData>(jsonString);
            }
            catch
            {
                return null;
            }
        }

    }
}
