using MiniProjectTwo.Model;
using MiniProjectTwo.Utilities;

namespace MiniProjectTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            RunApp();
        }

        private static void RunApp()
        {
            // Create options on first run
            Util.CreateCategoriesOffices();

            // Intro header
            Misc.LexiconHeader();

            // views
            Controller.Run();
        }
    }
}
