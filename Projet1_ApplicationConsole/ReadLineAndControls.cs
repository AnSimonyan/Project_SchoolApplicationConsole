using Projet1_ApplicationConsole.App;
using Projet1_ApplicationConsole.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1_ApplicationConsole
{
    public static class ReadLineAndControls
    {
        public static string ReadStringWithNullAndSpasesControl(string stringToDisplay)
        {
            string stringEntered;
            do
            {
                Console.Write(stringToDisplay);
                stringEntered = Console.ReadLine();

            } while (stringEntered == null || stringEntered.Trim() == "");

            return stringEntered.Trim();
        }
        public static DateTime GettingBirthDateInDateTimeFormat()
        {
            DateTime dateOfBirth;
            string input;
            do
            {
                Console.Write("Birthday (in format dd.mm.yyy): ");
                input = Console.ReadLine();
            }
            while (!DateTime.TryParseExact(input, "dd.MM.yyyy", null, DateTimeStyles.None, out dateOfBirth) || dateOfBirth > DateTime.Today || DateTime.Today.Year - dateOfBirth.Year > ConstantsAPP.MAXAGEOFSTUDENT);

            return dateOfBirth;
        }

        public static uint EnterNoteByCourse()
        {
            uint noteOfCourse;
            do
            {
                Console.Write("Enter note :");

            } while (!UInt32.TryParse(Console.ReadLine(), out noteOfCourse) || noteOfCourse > ConstantsAPP.MAXNOTE);

            return noteOfCourse;
        }
        public static string EnterAppreciation()
        {
            Console.Write("Enter appreciation :");
            return Console.ReadLine().Trim();
        }

        public static bool IsListIsEmpty<T>(List<T> list)
        {
            bool resultat = false;

            if (list.Count == 0)
            {
                Console.WriteLine("There are no elements added. Add at first elements ");
                resultat = true;
            }
            return resultat;
        }

        public static string SelectThePromotionFromList(List<Student> studentsPromo, AppData _appDataInitialised)
        {
            string promotion = "";
            do
            {
                Console.Write("To select a promotion type corresponding name from the list following:\n");
                DisplayInformation.DisplayPromotions(studentsPromo);
                promotion = Console.ReadLine().ToUpper();

            } while (promotion == "" || DataTools.GetStudentsListByPromo(_appDataInitialised, promotion) == null);
            return promotion;
        }

        public static uint DisplayCouresForSelection(AppData appData)
        {
            uint idCourse = 0;
            do
            {
                Console.Write("To select a course type corresponding ID from the list following:\n");
                DisplayInformation.DisplayListOfCours(appData);

            } while (!UInt32.TryParse(Console.ReadLine(), out idCourse) || DataTools.FindCourseByIndex(idCourse, appData) == null);
            //while (!UInt32.TryParse(Console.ReadLine(), out idCourse) || DataTools.FindCourseByID(idCourse, appData) == null);

            return idCourse;
        }

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

        public static uint DisplayStudentsForSelection(AppData appData)
        {
            uint selectedIDStudent = 0;
            do
            {
                Console.Write("To select a student type corresponding ID from the list following:\n");
                DisplayInformation.DisplayListOfStudents(appData);
                Console.Write("ID of student:");

            } while (!UInt32.TryParse(Console.ReadLine(), out selectedIDStudent) || DataTools.FindStudentByIndex(selectedIDStudent, appData) == null);
            //while (!UInt32.TryParse(Console.ReadLine(), out selectedIDStudent) || DataTools.FindStudentByID(selectedIDStudent, appData) == null);


            return selectedIDStudent;
        }
    }
}
