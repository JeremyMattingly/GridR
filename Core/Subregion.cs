using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivTrak.Assessment.GridR.Core
{
    public class Subregion(uint id) : IEquatable<Subregion>
    {
        public uint Id { get; } = id;
        public Dictionary<Coordinate, Cell> Cells { get; } = [];

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
