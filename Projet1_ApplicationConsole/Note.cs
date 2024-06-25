using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1_ApplicationConsole
{
    public class Note
    {

        public Course Course { get; }

        // public Student Student { get;  }

        public double Value { get; set; }

        public string Appreciation { get; set; }

        public int CourseID { get; set; } = 0;


        public Note(Course courseToNote, double value, string appreciation = "")
        {
            Course = courseToNote;
            //Student = studentToNote;
            Value = value;
            Appreciation = appreciation;
            if (courseToNote != null)
            { CourseID = courseToNote.ID; }
        }

        public Course GetTheNoteCourse()
        {
            return Course;
        }

        public Course GetTheNoteCourseByID(List<Course> coursList)
        {
            return coursList.Find(x => x.ID == CourseID);
        }

        //public void DeleteNotesByCourse(Course course)
        //{

        //}
    }
}
