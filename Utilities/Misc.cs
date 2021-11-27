using System;

namespace MiniProjectTwo.Utilities
{
    class Misc
    {
        //JUST FOR FUN, LESSON 2
        public static void LexiconHeader(string color = "Red", string secondColor = "Gray")
        {
            Console.Title = "Lexicon :: Lesson #4";
            string title = @"
    ██╗     ███████╗██╗  ██╗██╗ ██████╗ ██████╗ ███╗   ██╗
    ██║     ██╔════╝╚██╗██╔╝██║██╔════╝██╔═══██╗████╗  ██║
    ██║     █████╗   ╚███╔╝ ██║██║     ██║   ██║██╔██╗ ██║
    ██║     ██╔══╝   ██╔██╗ ██║██║     ██║   ██║██║╚██╗██║
    ███████╗███████╗██╔╝ ██╗██║╚██████╗╚██████╔╝██║ ╚████║
    ╚══════╝╚══════╝╚═╝  ╚═╝╚═╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝
    Application Created By Hadi.zakzouk@gmail.com
";
            string subTitle = @"
    +-+-+-+-+-+-+ +-+-+
    |L|e|s|s|o|n| |#|4|
    +-+-+-+-+-+-+ +-+-+
";

            //Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
            Console.Write(title);
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), secondColor, true);
            Console.WriteLine(subTitle);
            Console.ResetColor();
        }
    }
}
