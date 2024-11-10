using ActivTrak.Assessment.GridR.Core;

namespace ActivTrak.Assessment.GridR.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int exampleId = 0;
                bool validChoice = false;

                DisplayExampleChoice();

                if (int.TryParse(Console.ReadLine(), out var choice))
                {
                    switch (choice)
                    {
                        case 1:
                            exampleId = 1;
                            validChoice = true;
                            break;
                        case 2:
                            exampleId = 2;
                            validChoice = true;
                            break;
                        default:
                            IncorrectSelection();
                            validChoice = false;
                            break;
                    }
                }
                else
                {
                    IncorrectSelection();
                }

                if (validChoice)
                {
                    (int[,] exampleArray, int threshold) = Grid.GetExampleArray(exampleId);
                    var grid = new Grid(exampleArray, threshold);

                    PrintGridInfo(grid);

                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
            }
        }

        private static void PrintGridInfo(Grid grid)
        {
            Console.Clear();

            foreach (var subregion in grid.Subregions)
            {
                Console.WriteLine($"Subregion: {subregion.Key}");
                Console.WriteLine($"Center of Mass: {subregion.Value.CenterOfMass}");
                Console.WriteLine("Subregion's Cells:");

                foreach (var cell in subregion.Value.Cells)
                {
                    Console.WriteLine($"{cell.Value}");
                }

                Console.WriteLine();
            }
        }

        private static bool IncorrectSelection()
        {
            Console.WriteLine();
            Console.WriteLine("Woops. That wasn't a correct response. Please try again.");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            return true;
        }

        private static void DisplayExampleChoice()
        {
            Console.Clear();

            Console.WriteLine("Welcome to GridR.");
            Console.WriteLine();
            Console.WriteLine("1) Example Array 1");
            Console.WriteLine("2) Example Array 2*");
            Console.WriteLine();
            Console.WriteLine("*Same as Example 1, except cell (3,3) is no longer interesting and (4,4) is 250, causing there to be two cells the same distance from average and equal Y values.");
            Console.WriteLine();
            Console.WriteLine("Enter the ID of the Example Array you'd like to use. (Either \"1\" or \"2\")");
            Console.WriteLine();
            Console.Write("Your selection: ");
        }
    }
}
