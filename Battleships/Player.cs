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

            var gameOverMan = _shipHealths.All(x => x == 0);

            return new FireResult(originalContents, gameOverMan, shipWasSunk);
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

                    var vertical = Convert.ToBoolean(_random.Next(2));

                    var maxCoords = Grid.GetXAndYMax(vertical, _shipHealths[shipIndex]);

                    var targetSquares = Grid.GetLineOfSquares(
                        new Coordinates(_random.Next(maxCoords.X), _random.Next(maxCoords.Y)),
                        vertical,
                        _shipHealths[shipIndex]).ToList();

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

        public void ManuallyPlaceShip(Coordinates coordinates, bool vertical, int shipIndex)
        {
            foreach (var square in Grid.GetLineOfSquares(coordinates, vertical, _shipHealths[shipIndex]))
            {
                square.Contents = SquareContents.Ship;
                square.ShipIndex = shipIndex;
            }
        }
    }
}