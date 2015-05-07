namespace Battleships
{
    public class FireResult
    {
        public FireResult(SquareContents squareContents, bool gameOverMan, bool shipWasSunk)
        {
            SquareContents = squareContents;
            GameOverMan = gameOverMan;
            ShipWasSunk = shipWasSunk;
        }

        public readonly SquareContents SquareContents;
        public readonly bool GameOverMan;
        public readonly bool ShipWasSunk;
    }
}