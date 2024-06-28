using Projet1_ApplicationConsole.App;
using Projet1_ApplicationConsole.Data;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static DateTime GetingBirthDateInDateTime()
        {
            DateTime dateOfBirth;
            string input;
            do
            {
                Console.Write("Birthday (in format dd.mm.yyy): ");
                input = Console.ReadLine();
            }
            while (!DateTime.TryParseExact(input, "dd.MM.yyyy", null, DateTimeStyles.None, out dateOfBirth));

            return dateOfBirth;
        }
        public static uint DisplayStudentsForSelection(AppData appData)
        {
            uint selectedIDStudent = 0;
            do
            {
                Console.Write("To select a student type corresponding ID from the list following:\n");
                Console.Write(ConstantsAPP.MESSAGELINESEPARATOR);
                DisplayInformation.DisplayListOfStudents(appData);
                Console.Write(ConstantsAPP.MESSAGELINESEPARATOR);
                Console.Write("ID of student:");


            } while (!UInt32.TryParse(Console.ReadLine(), out selectedIDStudent) || DataTools.FindStudentByID(selectedIDStudent, appData) == null);

            return selectedIDStudent;
        }
        public static void DisplayListOfStudents(AppData appData)
        {
            foreach (Student student in appData.StudentsList)
            {
                Console.WriteLine("ID  :{0,5}. First name:  {1,-10}  Last name:  {2,-20} ", student.ID, student.FirstName, student.LastName);
            }
            Log.Information("Consultation de la liste des élèves.");
        }

        #endregion <-----------------------------------------------------------STUDENTS

        #region COURSES ------------------------------------------------------------->
        public static bool WarningBeforeDeletingCourse()
        {
            bool resultat = false;
            ConsoleKey key = ConsoleKey.Add;
            do
            {
                Console.WriteLine("Enter Y to confirm or N to cancel deleting!");
                key = Console.ReadKey(true).Key;

            } while (key != ConsoleKey.Y && key != ConsoleKey.N);

            if (key == ConsoleKey.Y) resultat = true;

            return resultat;
        }

        public static uint EnterNoteByCourse()
        {
            uint noteOfCourse;
            do
            {
                Console.Write("Enter note :");

            } while (!UInt32.TryParse(Console.ReadLine(), out noteOfCourse) || noteOfCourse > 20);

            return noteOfCourse;
        }

        public static uint  DisplayCouresForSelection(AppData appData)
        {
            uint idCourse = 0;
            do
            {
                Console.Write("To select a course type corresponding ID from the list following:\n");
                DisplayInformation.DisplayListOfCours(appData);

            } while (!UInt32.TryParse(Console.ReadLine(), out idCourse) || DataTools.FindCourseByID(idCourse, appData) == null);

            return idCourse;
        }

        public static void DisplayListOfCours(AppData appData)
        {
            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);

            foreach (Course stucoursent in appData.CoursesList)
            {
                Console.WriteLine("ID  :{0}. Name: {1}", stucoursent.ID, stucoursent.Name);
            }

            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);
        }

        public static string EnterNameOfCourse()
        {
            Console.Clear();
            Console.WriteLine("Addind a new course.");
            Console.Write("Name of course: ");

            return Console.ReadLine();
        }
        #endregion <-----------------------------------------------------------COURSES

        #region NOTES ------------------------------------------------------------->
        public static string EnterAppreciation()
        {
            Console.Write("Enter appreciation :");
            return Console.ReadLine();
        }

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

            if (studentNotes != null) notesTotal = DisplayInformation.ShowNotesListWithNotesTotal(studentNotes, appData.CoursesList);

            if (studentNotes.Count != 0) avarageNotes = notesTotal / studentNotes.Count;

            Console.WriteLine("\t Moyenne : " + Convert.ToString(avarageNotes));

            Console.Write(ConstantsAPP.MESSAGELINESEPARATOR);
        }
        #endregion <-----------------------------------------------------------NOTES



    }

}
