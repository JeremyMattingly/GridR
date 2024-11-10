using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivTrak.Assessment.GridR.Core
{
    public record struct Coordinate(uint X, uint Y) : IEquatable<Coordinate>
    {
        public uint X = X; public uint Y = Y;

        public readonly bool Equals(Coordinate other)
        {
            return (other.X == X && other.Y == Y);
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
