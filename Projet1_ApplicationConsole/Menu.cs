using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1_ApplicationConsole
{
    public class Menu
    {
        

        private ConsoleKey key = ConsoleKey.Tab;
        private int startX { get; set; } = 1;
        private int startY { get; set; } = 1;

        private int length = 1; 

        public bool canCancel { get; set; }

        public Menu(bool canCancel)
       {
            //Enum userMenu { Students, Course, Exit };
            //CanCancel = canCancel;
          //  length = Enum.GetValues(userEnum).Length;
            

        }
        //public int MultipleChoice( Enum userEnum, int spacingPerLine = 18, int optionsPerLine = 4)
        //{
        //    int currentSelection = 0;

        //    Console.CursorVisible = false;
        //    int length = Enum.GetValues(userEnum.GetType()).Length;

        //    while (key != ConsoleKey.Enter)
        //    {
        //        Console.Clear();

        //        for (int i = 0; i < length; i++)
        //        {
        //            Console.SetCursorPosition(startX + (i % optionsPerLine) * spacingPerLine, startY + i / optionsPerLine);

        //            if (i == currentSelection)
        //                Console.ForegroundColor = ConsoleColor.Red;

        //            Console.Write(Enum.Parse(userEnum.GetType(), i.ToString()));

        //            Console.ResetColor();
        //        }

        //        key = Console.ReadKey(true).Key;

        //        switch (key)
        //        {
        //            case ConsoleKey.LeftArrow:
        //                {
        //                    if (currentSelection % optionsPerLine > 0)
        //                        currentSelection--;
        //                    break;
        //                }
        //            case ConsoleKey.RightArrow:
        //                {
        //                    if (currentSelection % optionsPerLine < optionsPerLine - 1)
        //                        currentSelection++;
        //                    break;
        //                }
        //            case ConsoleKey.UpArrow:
        //                {
        //                    if (currentSelection >= optionsPerLine)
        //                        currentSelection -= optionsPerLine;
        //                    break;
        //                }
        //            case ConsoleKey.DownArrow:
        //                {
        //                    if (currentSelection + optionsPerLine < length)
        //                        currentSelection += optionsPerLine;
        //                    break;
        //                }
        //            case ConsoleKey.Escape:
        //                {
        //                    if (canCancel)
        //                        return -1;
        //                    break;
        //                }
        //        }
        //    };

        //    Console.CursorVisible = true;

        //    return currentSelection;
        //}
    }
    
   

}
