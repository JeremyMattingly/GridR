using System.Collections.ObjectModel;

namespace ActivTrak.Assessment.GridR.Core.Test
{
    internal static class ExpectedCellsForExampleArray
    {
        private static readonly Dictionary<Coordinate, Cell> _cells;

        public static ReadOnlyDictionary<Coordinate, Cell> Cells { get; private set; }

        static ExpectedCellsForExampleArray()
        {
            _cells = GetCells();

            Cells = _cells.AsReadOnly();
        }

        private static Dictionary<Coordinate, Cell> GetCells()
        {
            Dictionary<Coordinate, Cell> expectedCells = [];

            expectedCells.Add(new(0, 0), new(new(0, 0), 0));
            expectedCells.Add(new(0, 1), new(new(0, 1), 115));
            expectedCells.Add(new(0, 2), new(new(0, 2), 5));
            expectedCells.Add(new(0, 3), new(new(0, 3), 15));
            expectedCells.Add(new(0, 4), new(new(0, 4), 0));
            expectedCells.Add(new(0, 5), new(new(0, 5), 5));
            expectedCells.Add(new(1, 0), new(new(1, 0), 80));
            expectedCells.Add(new(1, 1), new(new(1, 1), 210));
            expectedCells.Add(new(1, 2), new(new(1, 2), 0));
            expectedCells.Add(new(1, 3), new(new(1, 3), 5));
            expectedCells.Add(new(1, 4), new(new(1, 4), 5));
            expectedCells.Add(new(1, 5), new(new(1, 5), 0));
            expectedCells.Add(new(2, 0), new(new(2, 0), 45));
            expectedCells.Add(new(2, 1), new(new(2, 1), 60));
            expectedCells.Add(new(2, 2), new(new(2, 2), 145));
            expectedCells.Add(new(2, 3), new(new(2, 3), 175));
            expectedCells.Add(new(2, 4), new(new(2, 4), 95));
            expectedCells.Add(new(2, 5), new(new(2, 5), 25));
            expectedCells.Add(new(3, 0), new(new(3, 0), 95));
            expectedCells.Add(new(3, 1), new(new(3, 1), 5));
            expectedCells.Add(new(3, 2), new(new(3, 2), 250));
            expectedCells.Add(new(3, 3), new(new(3, 3), 250));
            expectedCells.Add(new(3, 4), new(new(3, 4), 115));
            expectedCells.Add(new(3, 5), new(new(3, 5), 5));
            expectedCells.Add(new(4, 0), new(new(4, 0), 170));
            expectedCells.Add(new(4, 1), new(new(4, 1), 230));
            expectedCells.Add(new(4, 2), new(new(4, 2), 245));
            expectedCells.Add(new(4, 3), new(new(4, 3), 185));
            expectedCells.Add(new(4, 4), new(new(4, 4), 165));
            expectedCells.Add(new(4, 5), new(new(4, 5), 145));
            expectedCells.Add(new(5, 0), new(new(5, 0), 145));
            expectedCells.Add(new(5, 1), new(new(5, 1), 220));
            expectedCells.Add(new(5, 2), new(new(5, 2), 140));
            expectedCells.Add(new(5, 3), new(new(5, 3), 160));
            expectedCells.Add(new(5, 4), new(new(5, 4), 250));
            expectedCells.Add(new(5, 5), new(new(5, 5), 250));

            return expectedCells;
        }
    }
}
