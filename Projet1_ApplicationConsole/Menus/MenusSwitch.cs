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
            Students, Cours, Promotions, Exit
        }

        enum MenueStudents
        {
            ListStudents, CreateNewStudent, ViewExistingStudent, AddNotes, Exit
        }

        public enum MenueCourses
        {
            CoursesList, AddCourse, DeleteCourse, Exit
        }
        public enum MenuePromotions
        {
           AddPromoToStudent, PromotionsList, PromotionStudents, AveragePerStudentsPromo, AveragePromotionPerCours , Exit
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
            else if (menuLevel == 2)
            {
                int input = Menu.MultipleChoice(new MenueCourses(), MenuLists.CourseMetuLevel1(), 18, 1);
                MenusSwitch.SwichMenueCourses(input, appData);
            }

            else
            {
                int input = Menu.MultipleChoice(new MenuePromotions(), MenuLists.MenuPromotionLevel1(), 18, 1);
                MenusSwitch.SwichMenuePromotions(input, appData);
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
            Console.WriteLine(" \n");
            switch ((MenueCourses)input)
            {
                case MenueCourses.CoursesList:
                    
                    DisplayInformation.DisplayListOfCours(appData);
                    ReturnMenuInputLevel(2, appData);

                    break;
                case MenueCourses.AddCourse:
                    
                    UserTools.CreateCourse(appData);
                    ReturnMenuInputLevel(2, appData);
                    break;

                case MenueCourses.DeleteCourse:
                   
                    UserTools.DeleteCourse(appData);
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
            Console.WriteLine(" \n");
            switch ((MenuScool)input)
            {
                case MenuScool.Students:
                   
                    MenuMain( appData,1);
                    break;
                case MenuScool.Cours:
                   
                    MenuMain( appData,2);
                    break;
                case MenuScool.Promotions:
                    
                    MenuMain(appData, 3);
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
            Console.WriteLine(" \n");
            switch ((MenueStudents)input)
            {
                case MenueStudents.ListStudents:
                   
                    Console.WriteLine("Your choice: Settings");
                    DisplayInformation.DisplayListOfStudents(appData);
                    ReturnMenuInputLevel(1, appData);
                    break;
                case MenueStudents.CreateNewStudent:
                    
                    UserTools.CreateStudent(appData);
                    ReturnMenuInputLevel(1, appData);
                    break;

                case MenueStudents.ViewExistingStudent:
                    
                    UserTools.StudentInformation(appData);
                    ReturnMenuInputLevel(1, appData);
                    break;
                case MenueStudents.AddNotes:
                    
                    UserTools.AddNotes(appData);
                    ReturnMenuInputLevel(1, appData);
                    break;
                case MenueStudents.Exit:
                    MenuMain(appData,0);
                    break;
                default:
                    break;
            }
        }

        public static void SwichMenuePromotions(int input, AppData appData)
        {
            Console.WriteLine(" \n");
            switch ((MenuePromotions)input)
            {
                case MenuePromotions.AddPromoToStudent:
                    
                    UserTools.AddPromotionToStudent(appData);
                    ReturnMenuInputLevel(3, appData);
                    break;
                case MenuePromotions.PromotionsList:
                    
                    UserTools.PromotionsList(appData);
                    ReturnMenuInputLevel(3, appData);
                    break;
                case MenuePromotions.PromotionStudents:

                    UserTools.StudentsListByPromotions(appData);
                    ReturnMenuInputLevel(3, appData);
                    break;

                case MenuePromotions.AveragePerStudentsPromo:

                    UserTools.CoursesAvarageByPromotions(appData);
                    ReturnMenuInputLevel(3, appData);
                    break;
                case MenuePromotions.AveragePromotionPerCours:
                   
                   // new UserTools(appData).AddNotes();
                    ReturnMenuInputLevel(3, appData);
                    break;
                case MenuePromotions.Exit:
                    MenuMain(appData, 0);
                    break;
                default:
                    break;
            }
        }


    }
}
