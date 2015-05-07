namespace Battleships
{
    public struct Coordinates
    {
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Coordinates other)
        {
            if (X != other.X)
                return false;

            return Y == other.Y;
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public static bool operator ==(Coordinates point1, Coordinates point2)
        {
            return point1.Equals(point2);
        }

        public static bool operator !=(Coordinates point1, Coordinates point2)
        {
            return !point1.Equals(point2);
        } 

        public readonly int X;
        public readonly int Y;
    }
}