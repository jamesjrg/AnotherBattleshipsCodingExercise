using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    public static class InputOutputMapper
    {
        public static bool MapInputToArrayCoordinates(string input, out Coordinates coordinates, int arraySize)
        {
            coordinates = new Coordinates(0, 0);

            var normalizedInput = input.Trim().ToLower();

            if (normalizedInput.Length < 2)
                return false;

            var x = normalizedInput[0] - 97; //a switch statement would be more explicit but this is only a very simple program
            if (x < 0 || x > arraySize - 1)
                return false;

            var y = 0;
            if (!int.TryParse(normalizedInput.Substring(1), out y))
                return false;

            y = y - 1; //C# arrays are 0 indexed, the game is traditionally 1 indexed

            if (y < 0 || y > arraySize - 1) return false;

            coordinates = new Coordinates(x, y);
            return true;
        }

        public static IEnumerable<string> MapOutputToStrings(FireResult result)
        {
            yield return string.Format("Target square was: {0}", result.SquareContents);
            if (result.ShipWasSunk)
                yield return "Ship sunk!";
            if (result.GameOver)
                yield return "Game over man!";
        }
    }
}
