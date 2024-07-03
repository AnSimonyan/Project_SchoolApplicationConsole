using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1_ApplicationConsole.App
{
    public class Note
    {
        public int CourseID { get; }

       // public Course Course { get; }

        public double Value { get; }

       
        
        public string Appreciation { get; set; }

        //  public Note(Course courseToNote, double value, string appreciation = "")
        public Note(int courseID, double value, string appreciation = "")
        {
            //Course = courseToNote;
            //Student = studentToNote;
            CourseID = courseID;
            Value = value;
            Appreciation = appreciation;
           // if (courseToNote != null) CourseID = courseToNote.ID;

        }

        //public Course GetTheNoteCourse()
        //{
            //return Course;
        //}

        public Course GetTheNoteCourseByID(List<Course> coursList)
        {
            return coursList.Find(x => x.ID == CourseID);
        }
    }
}
