using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivTrak.Assessment.GridR.Core
{
    public class Cell(Coordinate coordinate, int value) : IEquatable<Cell>
    {
        public Coordinate GridCoordinate { get; } = coordinate;
        public int Value { get; set; } = value;

        public bool Equals(Cell? other) => other != null && other.GridCoordinate == GridCoordinate && other.Value == Value;

        public override bool Equals(object? obj)
        {
            return Equals(obj as Cell);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
