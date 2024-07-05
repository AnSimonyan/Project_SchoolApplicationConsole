using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1_ApplicationConsole
{
    public static class ConstantsAPP
    {
        public const string MESSAGEADDCONTINUE          = "Adding a new {0}...";
        public const string MESSAGELINESEPARATOR        = "\n------------------------------------------------------------------------------------------------\n";
        public const string MESSAGEDELETECOURSE         = "You want to delete a course. Select the ID of cours for delete, please. ";
        public const string MESSAGEWARNINGDELETECOURSE  = "Every note relating to this course will be deleted! Are you sure you want to delete the course {0}? Y/N";
       
        public const string JSONFILENAME    = "H:/Франция/WebDev/PO/Projet1_ApplicationConsole/Json/";
        public const string LOGPATH         = "H:/Франция/WebDev/PO/Projet1_ApplicationConsole/Log.txt";

        public const int MAXNOTE = 20;
        public const int MAXAGEOFSTUDENT = 100;
    }
}
