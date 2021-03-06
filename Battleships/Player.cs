using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships
{
    public class Player
    {
        public readonly Grid Grid;
        private readonly List<int> _shipHealths;
        private readonly Random _random = new Random();

        public Player(int gridSize, Dictionary<int, int> startingShips, bool doRandomPlace)
        {
            Grid = new Grid(gridSize);

            _shipHealths = startingShips.SelectMany(kv => Enumerable.Range(0, kv.Value).Select(x => kv.Key)).ToList();

            if (doRandomPlace)
                PlaceRandomShips();
        }

        public FireResult FiredAt(Coordinates coordinates)
        {
            var targetSquare = Grid.Squares[coordinates.X, coordinates.Y];
            var originalContents = targetSquare.Contents;
            var shipWasSunk = false;

            if (targetSquare.Contents == SquareContents.Ship)
            {
                targetSquare.Contents = SquareContents.DamagedShip;
                _shipHealths[targetSquare.ShipIndex]--;
                shipWasSunk = _shipHealths[targetSquare.ShipIndex] == 0;
            }

            var gameOver = _shipHealths.All(x => x == 0);

            return new FireResult(originalContents, gameOver, shipWasSunk);
        }

        private void PlaceRandomShips()
        {
            for (var shipIndex = 0; shipIndex != _shipHealths.Count; ++shipIndex)
            {
                var placed = false;
                var attemptCount = 0;
                while (!placed)
                {
                    if (attemptCount == 1000)
                        throw new Exception("Failed to place a ship after 1000 attempts");

                    var lineType = Convert.ToBoolean(_random.Next(2))
                        ? LineType.Vertical
                        : LineType.Horizontal;

                    var maxCoords = Grid.GetXAndYMax(lineType, _shipHealths[shipIndex]);

                    var targetSquares = Grid.GetLineOfSquares(
                        new Coordinates(_random.Next(maxCoords.X), _random.Next(maxCoords.Y)),
                        lineType,
                        _shipHealths[shipIndex]);

                    if (targetSquares.All(square => square.Contents == SquareContents.Sea))
                    {
                        placed = true;
                        foreach (var square in targetSquares)
                        {
                            square.Contents = SquareContents.Ship;
                            square.ShipIndex = shipIndex;
                        }
                    }

                    attemptCount++;
                }
            }
        }

        public void ManuallyPlaceShip(Coordinates coordinates, LineType vertical, int shipIndex)
        {
            foreach (var square in Grid.GetLineOfSquares(coordinates, vertical, _shipHealths[shipIndex]))
            {
                square.Contents = SquareContents.Ship;
                square.ShipIndex = shipIndex;
            }
        }
    }
}