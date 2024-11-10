using System.Collections.ObjectModel;

namespace ActivTrak.Assessment.GridR.Core
{
    public class Subregion : IEquatable<Subregion>
    {
        private readonly Dictionary<Coordinate, Cell> _cells = [];

        public uint Id { get; }
        public ReadOnlyDictionary<Coordinate, Cell> Cells { get; private set; }

        public Subregion(uint id)
        {
            Id = id;
            Cells = _cells.AsReadOnly();
        }

        public void AddCell(Coordinate coordinate, Cell cell)
        {
            _cells.Add(coordinate, cell);
        }

        public bool Equals(Subregion? other) => other != null && other.Id == Id;

        public override bool Equals(object? obj)
        {
            return Equals(obj as Subregion);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Subregion? left, Subregion? right)
        {
            if (left is null || right is null) return Object.Equals(left, right);
            return left.Id == right.Id;
        }

        public static bool operator !=(Subregion? left, Subregion? right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
