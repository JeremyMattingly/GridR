using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivTrak.Assessment.GridR.Core
{
    public class Grid
    {
        private readonly int[,] _sourceArray;

        public List<Cell> Cells { get; } = [];

        public Grid (int[,] sourceArray)
        {
            if (sourceArray.Length == 0) throw new ArgumentException($"{nameof(sourceArray)} cannot be empty.");
            if (sourceArray.GetLength(0) != sourceArray.GetLength(1)) throw new ArgumentException($"{nameof(sourceArray)} dimension lengths must match.");

            _sourceArray = sourceArray;

            LoadCells();
        }

        private void LoadCells()
        {
            for (uint x = 0; x < _sourceArray.GetLength(0); x++)
            {
                for (uint y = 0; y < _sourceArray.GetLength(1); y++)
                {
                    Cells.Add(new Cell(new Coordinate(x, y), _sourceArray[x, y]));
                }
            }
        }
    }
}
