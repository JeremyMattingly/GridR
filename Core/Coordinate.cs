namespace ActivTrak.Assessment.GridR.Core
{
    public readonly record struct Coordinate(uint X, uint Y) : IEquatable<Coordinate>
    {
        public readonly bool Equals(Coordinate other)
        {
            return (other.X == X && other.Y == Y);
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString() => $"({X}, {Y})";
    }
}
