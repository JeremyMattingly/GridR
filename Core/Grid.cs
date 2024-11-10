namespace ActivTrak.Assessment.GridR.Core
{
    public class Grid
    {
        private readonly int[,] _sourceArray;
        private readonly Coordinate _maxCoordinate;

        public Dictionary<Coordinate, Cell> Cells { get; } = [];
        public int Threshold { get; }
        public Dictionary<uint, Subregion> Subregions { get; } = [];

        public Coordinate MaxCoordinate => _maxCoordinate;

        public Grid(int[,] sourceArray, int threshold)
        {
            if (sourceArray.Length == 0) throw new ArgumentException($"{nameof(sourceArray)} cannot be empty.");
            if (sourceArray.GetLength(0) != sourceArray.GetLength(1)) throw new ArgumentException($"{nameof(sourceArray)} dimension lengths must match.");

            _sourceArray = sourceArray;

            _maxCoordinate = new Coordinate((uint)_sourceArray.GetLength(0), (uint)_sourceArray.GetLength(1));

            Threshold = threshold;

            LoadCells();

            Subregions = Subregion.GetSubregionsForGrid(this);
        }

        public static (int[,] exampleArray, int threshold) GetExampleArray()
        {
            int threshold = 200;

            int[,] input = new int[6, 6]
            {
                { 0, 115, 5, 15, 0, 5 },
                { 80, 210, 0, 5, 5, 0 },
                { 45, 60, 145, 175, 95, 25 },
                { 95, 5, 250, 250, 115, 5 },
                { 170, 230, 245, 185, 165, 145 },
                { 145, 220, 140, 160, 250, 250 }
            };
            return (input, threshold);
        }

        private void LoadCells()
        {
            for (uint x = 0; x < _sourceArray.GetLength(0); x++)
            {
                for (uint y = 0; y < _sourceArray.GetLength(1); y++)
                {
                    Coordinate coordinate = new(x, y);
                    Cells.Add(coordinate, new Cell(coordinate, _sourceArray[x, y]));
                }
            }
        }


    }
}
