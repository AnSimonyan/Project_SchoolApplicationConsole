using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace Projet1_ApplicationConsole.Menus
{
    public class Menu
    {       
        public Menu(bool canCancel, AppData appTools, int level = 0)
        {
            while (true)
            {
                MenusSwitch.MenuMain(appTools,level );

                Console.ReadLine();
                Console.Clear();

            }
        }

        public static int MultipleChoice(Enum userEnum, List<string> menueFullName, int spacingPerLine = 18, int optionPerLine = 4)
        {
            int currentSelection = 0; ConsoleKey key;
            int startX = 1; int startY = 1;

            Console.CursorVisible = false;

            int length = Enum.GetValues(userEnum.GetType()).Length;
            do
            {
                Console.Clear();

                for (int i = 0; i < length; i++)
                {
                    Console.SetCursorPosition(startX + i % optionPerLine * spacingPerLine, startY + i / optionPerLine);

                    if (i == currentSelection) Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.Write(menueFullName[i]);

                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;
                currentSelection = SwichKeyArrows(key, currentSelection, length, optionPerLine);

            } while (key != ConsoleKey.Enter);

            Console.CursorVisible = true;

            return currentSelection;
        }

        private static  int SwichKeyArrows(ConsoleKey key, int currentSelection, int length, int optionPerLine)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    {
                        if (currentSelection % optionPerLine > 0)
                            currentSelection--;
                        return currentSelection;
                        break;
                    }
                case ConsoleKey.RightArrow:
                    {
                        if (currentSelection % optionPerLine < optionPerLine - 1)
                            currentSelection++;
                        return currentSelection;
                        break;
                    }
                case ConsoleKey.UpArrow:
                    {
                        if (currentSelection >= optionPerLine)
                            currentSelection -= optionPerLine;
                        return currentSelection;
                        break;
                    }
                case ConsoleKey.DownArrow:
                    {
                        if (currentSelection + optionPerLine < length)
                            currentSelection += optionPerLine;
                        return currentSelection;
                        break;
                    }
                case ConsoleKey.Escape:
                    {
                        return -1;
                        break;
                    }
            }
            return currentSelection;
        }
              
    }

}
