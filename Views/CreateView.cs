using MiniProjectTwo.Utilities;

namespace MiniProjectTwo.Views
{
    class CreateView
    {
        public static void Run()
        {
            CreateOrUpdateProductForm.Run();

            // Show data table after adding new product
            Util.ClearConsole();
            ProductDataTable.Run();
        }
    }
}
