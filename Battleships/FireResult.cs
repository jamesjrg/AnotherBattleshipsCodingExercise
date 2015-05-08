namespace Battleships
{
    public class FireResult
    {
        public FireResult(SquareContents squareContents, bool gameOver, bool shipWasSunk)
        {
            SquareContents = squareContents;
            GameOver = gameOver;
            ShipWasSunk = shipWasSunk;
        }

        public readonly SquareContents SquareContents;
        public readonly bool GameOver;
        public readonly bool ShipWasSunk;
    }
}