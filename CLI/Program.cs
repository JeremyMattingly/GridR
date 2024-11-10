using ActivTrak.Assessment.GridR.Core;

namespace ActivTrak.Assessment.GridR.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] inputArray = GetArray();
            int threshold = 200;


            // Get Grid (array, threshold)
            var grid = new Grid(inputArray, threshold);

            // Display sub-regions
            foreach (var subregion in grid.Subregions)
            {
                Console.WriteLine($"Subregion: {subregion.Key}");
                // with their interesting cells
                foreach (var cell in subregion.Value.Cells)
                {
                    Console.WriteLine($"{cell.Value.ToString()}");
                }
            }
            Console.ReadKey();

            
        }

        private static int[,] GetArray()
        {
            int[,] input = new int[6, 6]
            {
                { 0, 115, 5, 15, 0, 5 },
                { 80, 210, 0, 5, 5, 0 },
                { 45, 60, 145, 175, 95, 25 },
                { 95, 5, 250, 250, 115, 5 },
                { 170, 230, 245, 185, 165, 145 },
                { 145, 220, 140, 160, 250, 250 }
            };
            return input;
        }
    }
}
