using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace Projet1_ApplicationConsole
{
    public class Menu
    {
        public List<Student> StudentsList { get; }
       
        public List<Course> CoursesList { get; }

        private UserTools userTools; 
        
        enum MenuScool
        {
            Students, Cours, Exit
        }
        
        enum MenueStudents
        {
            ListStudents, CreateNewStudent, ViewExistingStudent, AddNotes, Exit
        }
        
        enum MenueCourses
        {
            CoursesList, AddCourse, DeleteCourse, Exit
        }

        public Menu(bool canCancel, List<Student> studentsList, List<Course> courseList, int level = 0)
        {
            this.StudentsList = studentsList;
            this.CoursesList = courseList;
            userTools = new UserTools(studentsList, courseList);
            while (true)
            {
                MenuMain(level);

                Console.ReadLine();
                Console.Clear();

            }
        }

        public int MultipleChoice(bool canCancel, Enum userEnum, List<string> menueFullName, int spacingPerLine = 18, int optionsPerLine = 3, int startX = 1, int startY = 1)
        {
            int currentSelection = 0;
            ConsoleKey key;
            Console.CursorVisible = false;
            int length = Enum.GetValues(userEnum.GetType()).Length;
            do
            {
                Console.Clear();

                for (int i = 0; i < length; i++)
                {
                    Console.SetCursorPosition(startX + (i % optionsPerLine) * spacingPerLine, startY + i / optionsPerLine);

                    if (i == currentSelection)
                        Console.ForegroundColor = ConsoleColor.Yellow;

                    // Console.Write(Enum.Parse(userEnum.GetType(), i.ToString()));
                    Console.Write(menueFullName[i]);

                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            if (currentSelection % optionsPerLine > 0)
                                currentSelection--;
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            if (currentSelection % optionsPerLine < optionsPerLine - 1)
                                currentSelection++;
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            if (currentSelection >= optionsPerLine)
                                currentSelection -= optionsPerLine;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (currentSelection + optionsPerLine < length)
                                currentSelection += optionsPerLine;
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            if (canCancel)
                                return -1;
                            break;
                        }
                }
            } while (key != ConsoleKey.Enter);

            Console.CursorVisible = true;

            return currentSelection;
        }

        private List<string> StudentsMetuLevel1()
        {
            List<string> listMenuStudents = new List<string>();
            listMenuStudents.Add("Lister les élèves");
            listMenuStudents.Add("Créer un nouvel élève");
            listMenuStudents.Add("Consulter un élève existant");
            listMenuStudents.Add("Ajouter une note et une appréciation pour un cours sur un élève existant");
            listMenuStudents.Add("Revenir au menu principal");

            return listMenuStudents;

        }
        
        private List<string> MenuScoolList()
        {
            List<string> listMenuScool = new List<string>();
            listMenuScool.Add("Elèves");
            listMenuScool.Add("Cours");
            listMenuScool.Add("Sortir");

            return listMenuScool;

        }
       
        private List<string> CourseMetuLevel1()
        {
            List<string> listMenuScool = new List<string>();
            listMenuScool.Add("Lister les cours existants");
            listMenuScool.Add("Ajouter un nouveau cours au programme");
            listMenuScool.Add("Supprimer un cours par son identifiant");
            listMenuScool.Add("Revenir au menu principal");
            return listMenuScool;
        }

        private void MenuMain(int menuLevel = 0)

        {
            if (menuLevel == 0)
            {
                int input = MultipleChoice(true, new MenuScool(), MenuScoolList());
                SwichMenueScool(input);
            }
            else if (menuLevel == 1)
            {
                int input = MultipleChoice(true, new MenueStudents(), StudentsMetuLevel1(), 18, 1);
                SwichMenueStudents(input);
            }
            else
            {
                int input = MultipleChoice(true, new MenueCourses(), CourseMetuLevel1(), 18, 1);
                SwichMenueCourses(input);
            }


        }

        private void SwichMenueScool(int input)
        {
            switch ((MenuScool)input)
            {
                case MenuScool.Students:
                    Console.WriteLine(" \n");
                    
                    MenuMain(1);
                    break;
                case MenuScool.Cours:
                    Console.WriteLine(" \n");
                   
                    MenuMain(2);
                    break;

                case MenuScool.Exit:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        private void SwichMenueStudents(int input)
        {
            switch ((MenueStudents)input)
            {
                case MenueStudents.ListStudents:
                    Console.WriteLine(" \n");
                    Console.WriteLine("Your choice: Settings");
                    userTools.DisplayListOfStudents();
                    ReturnMenuInputLevel(1);
                    break;
                case MenueStudents.CreateNewStudent:
                    Console.WriteLine(" \n");
                    userTools.CreateStudent();
                    ReturnMenuInputLevel(1);
                    break;

                case MenueStudents.ViewExistingStudent:
                    Console.WriteLine(" \n");
                    userTools.StudentInformation();
                    ReturnMenuInputLevel(1);
                    break;
                case MenueStudents.AddNotes:
                    Console.WriteLine(" \n");

                    userTools.AddNotes();
                    ReturnMenuInputLevel(1);
                    break;
                case MenueStudents.Exit:
                    MenuMain(0);
                    break;
                default:
                    break;
            }
        }

        private void SwichMenueCourses(int input)
        {
            
            switch ((MenueCourses)input)
            {
                case MenueCourses.CoursesList:
                    Console.WriteLine(" \n");
                    //Console.WriteLine("Your choice: Settings");
                    userTools.DisplayListOfCours();
                    ReturnMenuInputLevel(2);

                    break;
                case MenueCourses.AddCourse:
                    Console.WriteLine(" \n");
                    userTools.CreateCourse();
                    ReturnMenuInputLevel(2);
                    break;

                case MenueCourses.DeleteCourse:
                    Console.WriteLine(" \n");
                    userTools.DeleteCourse();
                    ReturnMenuInputLevel(2);
                    break;

                case MenueCourses.Exit:
                    MenuMain(0);
                    break;
                default:
                    break;

                   
            }
        }

        private void ReturnMenuInputLevel(int inputLevel)
        {
            Console.WriteLine("Press ENTER to continue...");
            ConsoleKey key = ConsoleKey.N;
            do
            {
                key = Console.ReadKey(true).Key;
                

            } while (key != ConsoleKey.Enter);
            MenuMain(inputLevel);
        }

    }

}
