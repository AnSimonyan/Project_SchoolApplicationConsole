using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet1_ApplicationConsole;

namespace Projet1_ApplicationConsole.Menus
{
    public  class MenusSwitch
    {
        public enum MenuScool
        {
            Students, Cours, Exit
        }

        enum MenueStudents
        {
            ListStudents, CreateNewStudent, ViewExistingStudent, AddNotes, Exit
        }

        public enum MenueCourses
        {
            CoursesList, AddCourse, DeleteCourse, Exit
        }
       
        public static void MenuMain(AppData appData, int menuLevel = 0)

        {
            if (menuLevel == 0)
            {
                int input = Menu.MultipleChoice(new MenuScool(), MenuLists.MenuScoolList());
                MenusSwitch.SwichMenueScool(input, appData);
            }
            else if (menuLevel == 1)
            {
                int input = Menu.MultipleChoice(new MenueStudents(), MenuLists.StudentsMetuLevel1(), 18, 1);
                MenusSwitch.SwichMenueStudents(input, appData);
            }
            else
            {
                int input = Menu.MultipleChoice(new MenueCourses(), MenuLists.CourseMetuLevel1(), 18, 1);
                MenusSwitch.SwichMenueCourses(input, appData);
            }
        }
                
        public static void ReturnMenuInputLevel(int inputLevel,AppData appTools)
        {
            Console.WriteLine("Press ENTER to continue...");
            ConsoleKey key = ConsoleKey.N;
            do
            {
                key = Console.ReadKey(true).Key;

            } while (key != ConsoleKey.Enter);
            MenusSwitch.MenuMain(appTools,inputLevel);
        }
        
        public static void SwichMenueCourses(int input, AppData appData)
        {
            switch ((MenueCourses)input)
            {
                case MenueCourses.CoursesList:
                    Console.WriteLine(" \n");
                    DisplayInformation.DisplayListOfCours(appData);
                    ReturnMenuInputLevel(2, appData);

                    break;
                case MenueCourses.AddCourse:
                    Console.WriteLine(" \n");
                    new UserTools(appData).CreateCourse();
                    ReturnMenuInputLevel(2, appData);
                    break;

                case MenueCourses.DeleteCourse:
                    Console.WriteLine(" \n");
                    new UserTools(appData).DeleteCourse();
                    ReturnMenuInputLevel(2, appData);
                    break;

                case MenueCourses.Exit:
                    MenuMain(appData,0);
                    break;
                default:
                    break;
            }
        }

        public static void SwichMenueScool(int input, AppData appData)
        {
            switch ((MenuScool)input)
            {
                case MenuScool.Students:
                    Console.WriteLine(" \n");

                    MenuMain( appData,1);
                    break;
                case MenuScool.Cours:
                    Console.WriteLine(" \n");

                    MenuMain( appData,2);
                    break;

                case MenuScool.Exit:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        public static void SwichMenueStudents(int input, AppData appData)
        {
            switch ((MenueStudents)input)
            {
                case MenueStudents.ListStudents:
                    Console.WriteLine(" \n");
                    Console.WriteLine("Your choice: Settings");
                    DisplayInformation.DisplayListOfStudents(appData);
                    ReturnMenuInputLevel(1, appData);
                    break;
                case MenueStudents.CreateNewStudent:
                    Console.WriteLine(" \n");
                    new UserTools(appData).CreateStudent();
                    ReturnMenuInputLevel(1, appData);
                    break;

                case MenueStudents.ViewExistingStudent:
                    Console.WriteLine(" \n");
                    new UserTools(appData).StudentInformation();
                    ReturnMenuInputLevel(1, appData);
                    break;
                case MenueStudents.AddNotes:
                    Console.WriteLine(" \n");
                    new UserTools(appData).AddNotes();
                    ReturnMenuInputLevel(1, appData);
                    break;
                case MenueStudents.Exit:
                    MenuMain(appData,0);
                    break;
                default:
                    break;
            }
        }

    }
}
