namespace ActivTrak.Assessment.GridR.Core
{
    public class Cell(Coordinate coordinate, int value) : IEquatable<Cell>
    {
        public Coordinate GridCoordinate { get; } = coordinate;
        public int Signal { get; } = value;

        public bool Equals(Cell? other) => (other != null) && (other.GridCoordinate == GridCoordinate) && (other.Signal == Signal);

        public override bool Equals(object? obj)
        {
            return Equals(obj as Cell);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override string ToString() => $"{GridCoordinate} {Signal}";
    }
}
