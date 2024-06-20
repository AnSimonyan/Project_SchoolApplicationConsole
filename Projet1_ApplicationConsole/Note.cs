using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1_ApplicationConsole
{
    internal class Note
    {
        public double ID { get; }
        public Course Course { get;  }

        public Student Student { get;  }

        public double Value { get; set; }

        public string Appreciation { get; set; }

        public Note (  Student studentToNote, Course courseToNote, double value, string appreciation ="" )
        {
            Course = courseToNote;
            Student = studentToNote;
            Value = value;
            Appreciation = appreciation;
        }

        public Course GetTheNoteCourse()
        {
            return Course;
        }
    }
}
