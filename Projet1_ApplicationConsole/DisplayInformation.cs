using Projet1_ApplicationConsole.App;
using Projet1_ApplicationConsole.Data;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Projet1_ApplicationConsole
{
    public static class DisplayInformation
    {
        #region STUDENTS ------------------------------------------------------------->
        public static void DisplayInformationForStudent(Student selectedStudent)
        {
            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);
            Console.WriteLine("Informations sur l'élève : \n ");
            Console.WriteLine("{0,-18} {1,-23}", "Nom :", selectedStudent.FirstName);
            Console.WriteLine("{0,-18} {1,-23}", "Prénom :", selectedStudent.LastName);
            Console.WriteLine("{0,-18} {1,-22}", "Date de naissance :", selectedStudent.DateOfBirth.ToString("d"));
        }   

        public static void DisplayListOfStudents(AppData appData)
        {
            if (appData == null) return;
            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);
            uint indexof = 0;
            foreach (Student student in appData.StudentsList)
            {
                //Console.WriteLine("ID  :{0,5}. First name:  {1,-10} \t   {2,-20} ", student.ID, student.FirstName, "Last name: " + student.LastName);
                Console.WriteLine("ID  :{0,5}. First name:  {1,-10} \t   {2,-20} ", indexof, student.FirstName, "Last name: " + student.LastName);
                indexof++;
            }

            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);
            Log.Information("Consultation de la liste des élèves.");

        }

        #endregion <-----------------------------------------------------------STUDENTS

        #region COURSES ------------------------------------------------------------->
        
        public static void DisplayListOfCours(AppData appData)
        {
            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);
            int indexof = 0;
            foreach (Course stucoursent in appData.CoursesList)
            {
                // Console.WriteLine("ID  :{0}. Name: {1}", stucoursent.ID, stucoursent.Name);
                Console.WriteLine("ID  :{0}. Name: {1}", indexof, stucoursent.Name);
                indexof++;
            }

            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);
        }
        #endregion <-----------------------------------------------------------COURSES

        #region NOTES ------------------------------------------------------------->
        public static double ShowNotesListWithNotesTotal(List<Note> studentNotes, List<Course> coursesList)
        {
            double notesTotal = 0;

            foreach (Note note in studentNotes)
            {
                Console.WriteLine("\t Cours : " + note.GetTheNoteCourseByID(coursesList).Name);
                Console.WriteLine("\t \t Note : " + note.Value + "/" + Convert.ToString(ConstantsAPP.MAXNOTE));
                Console.WriteLine("\t \t Appréciation : " + note.Appreciation + "\n");

                notesTotal = notesTotal + note.Value;
            }
            return notesTotal;
        }

        public static void DisplayInformationForStudentNotes(Student selectedStudent, AppData appData)
        {
            double notesTotal = 0; double avarageNotes = 0;
                      
            Console.WriteLine("Résultats scolaires:\n");

            List<Note> studentNotes = selectedStudent.GetStudentsNotes();

            if (studentNotes.Count == 0) return; 

            if (studentNotes != null) notesTotal = DisplayInformation.ShowNotesListWithNotesTotal(studentNotes, appData.CoursesList);

            avarageNotes = notesTotal / studentNotes.Count;

            Console.WriteLine("\t Moyenne : " + Convert.ToString(avarageNotes));

            Console.Write(ConstantsAPP.MESSAGELINESEPARATOR);
        }
        #endregion <-----------------------------------------------------------NOTES

        #region PROMOTIONS--------------------------------------------------------->
        public static void DisplayStudentInformationByPromotions(List<Student> studentsPromo)
        {
            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);
            foreach (Student student  in studentsPromo)
            { 
                Console.WriteLine("ID  :{0,5}. First name:  {1,-10} \t Last name:  {2,-20}  \t Promotion: {3,-30} ", student.ID, student.FirstName, student.LastName, student.GetPromotion());
            }
            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);
        }
        public static void DisplayPromotions(List<Student> studentsPromo)
        {
            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);
            var promos = DataTools.GetUniqPomotionListForStudentList(studentsPromo);
           foreach (string promo in promos)
            {
                Console.WriteLine("Promotion: {0,-10} ", promo);
            }
            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);
        }

        public static void DisplayStudentWasChanged(Student student)
        {
            Console.WriteLine("The studant: " + student.FirstName + " " + student.LastName + " details was updated!"); 
        }

        public static void DisplayCoursesPromo(Dictionary<Course, List<double>> coursesPromo)
        {
            Course[] coursKeys = coursesPromo.Keys.ToArray();
            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);
            foreach (Course course  in coursKeys)
            {
                Console.WriteLine("Course  : " + course.Name + ": "+coursesPromo[course].Average());
            }
            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);
        }

        public static void DisplayPromoAvarageByCourse(List<Student> students,List<Course> courseListDistinct, List<string> promoStudentsDistinct)
        {
            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);
            foreach (Course course in courseListDistinct)
            {
                string textForConsole = "Course  :"+ course.Name +": ";

                foreach (string promo in promoStudentsDistinct)
                {
                    var noteList = DataTools.GetNotesValuesByCouresIdAndPromoFromStudentsList(students, promo, course.ID);
                    if (noteList.ToList().Count>0)  textForConsole = textForConsole +"\n \t "+ promo + ": " + noteList.ToList().Average();
                }

                Console.WriteLine(textForConsole);
            }
            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);
        }
        #endregion <------------------------------------------------------PROMOTIONS
    }

}
