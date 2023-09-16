namespace CodingTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseFunctions.CreateDB();
            Menu.ShowMenu();
        }
    }
}