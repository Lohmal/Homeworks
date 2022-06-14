using Figgle;
using System;
using System.Drawing;

using static System.Console;
namespace menu
{
    class Menu
    {
        private int SelectedIndex;
        private string[] Options;
        private string Prompt;

        public Menu(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedIndex = 0;
        }

        private void DisplayOptions()
        {
            if (Prompt == "                                                                       Bir Zamanlar Chicago")
            {
                ForegroundColor = ConsoleColor.Red;
                Logo();
            }
            else
            {
                ForegroundColor = ConsoleColor.Magenta;
            }
            WriteLine(Prompt);
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string Lprefix;
                string Rprefix;
                if (i == SelectedIndex)
                {
                    Lprefix = "<<";
                    Rprefix = ">>";
                    ForegroundColor = ConsoleColor.White;
                    //ForegroundColor = ConsoleColor.Black;
                    //BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Lprefix = " ";
                    Rprefix = " ";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", $"{Lprefix} {currentOption} {Rprefix}"));
            }
            ResetColor();
        }
        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);

            return SelectedIndex;
        }
        //Logo
        public static void Logo()
        {
            Console.WriteLine(FiggleFonts.Standard.Render("                                             Mask of Silence"));
            ForegroundColor = ConsoleColor.Green;
        }
    }
}