namespace Battleships
{
    public enum SquareContents
    {
        Sea,
        Ship,
        DamagedShip
    }

    public class Square
    {
        public SquareContents Contents { get; set; }
        public int ShipIndex { get; set; }
    }
}