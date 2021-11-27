using MiniProjectTwo.Utilities;
using MiniProjectTwo.Views;
using System;

namespace MiniProjectTwo.Model
{
    class Controller
    {
        public static void Run()
        {
            ConsoleKeyInfo cki;
            Menu.Run();

            while (true)
            {
                cki = Console.ReadKey(true);
                Util.ClearConsole();

                ViewController(cki);
            }
        }

        private static void ViewController(ConsoleKeyInfo cki)
        {
            switch (cki.Key)
            {
                case ConsoleKey.C or ConsoleKey.Add:
                    CreateView.Run();
                    break;
                case ConsoleKey.U or ConsoleKey.Insert:
                    UpdateView.Run();
                    break;
                case ConsoleKey.R:
                    ProductDataTable.Run();
                    break;
                case ConsoleKey.Delete or ConsoleKey.D:
                    DeleteView.Run();
                    break;
                case ConsoleKey.S:
                    Util.Search();
                    break;
                case ConsoleKey.Print or ConsoleKey.N:
                    Util.DemoData();
                    break;
                case ConsoleKey.Escape:
                    Util.ExitApp();
                    break;
            }
        }

    }
}
