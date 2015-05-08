using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    public class Grid
    {
        public readonly Square[,] Squares;

        public Grid(int gridSize)
        {
            Squares = new Square[gridSize, gridSize];

            for (var i = 0; i != gridSize; ++i)
                for (var j = 0; j != gridSize; ++j)
                    Squares[i, j] = new Square { Contents = SquareContents.Sea };
        }

        public Coordinates GetXAndYMax(bool vertical, int shipHealth)
        {
            var gridSize = Squares.GetLength(0);

            var xMax = vertical ? gridSize : gridSize - shipHealth + 1;
            var yMax = vertical ? gridSize - shipHealth + 1 : gridSize;

            return new Coordinates(xMax, yMax);
        }

        public IList<Square> GetLineOfSquares(Coordinates startingCoordinates, bool vertical, int shipHealth)
        {
            return Enumerable.Range(0, shipHealth).Select(i =>
            {
                var xCoord = vertical ? startingCoordinates.X : startingCoordinates.X + i;
                var yCoord = vertical ? startingCoordinates.Y + i : startingCoordinates.Y;
                return Squares[xCoord, yCoord];
            }).ToList();
        }
    }
}
