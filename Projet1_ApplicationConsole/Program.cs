
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
            UserTools userTools = new UserTools(StudentsList, CoursesList);
            userTools.UserAddTheStudent();
            userTools.UserAddTheCourse();
            userTools.UserAddNotes();

            foreach (Course course in CoursesList)
            {
                Console.WriteLine("Course: ID: {0}, Name: {1}", course.ID,course.Name);
            }


            foreach (Student student in StudentsList)
            {
                Console.WriteLine("Student: ID:{0}, First name:{1}, Last name:{2}", student.ID, student.FirstName, student.LastName);
                List<Note> notes = student.GetStudentsNotes();
                foreach (Note note in notes)
                {
                    Console.WriteLine("Cours : {0}, Note value : {1}, Note appreciation : {2}", note.Course, note.Value, note.Appreciation);
                }
                
            }


        }   



    }


}

