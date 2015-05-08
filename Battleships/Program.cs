using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            const int gridSize = 10;
            var startingShips = new Dictionary<int, int>
            {
                {5, 1},
                {4, 2}
            };
            var random = new Random();

            var human = new Player(gridSize, startingShips, true);
            var computer = new Player(gridSize, startingShips, true);

            while (true)
            {
                Console.WriteLine("Enter target coordinates (e.g. H5), or enter nothing to exit");
                var inputLine = Console.ReadLine();
                if (inputLine == "") return;

                Coordinates coordinates;
                if (!InputOutputMapper.MapInputToArrayCoordinates(inputLine, out coordinates, gridSize))
                {
                    Console.WriteLine("Invalid input, please try again");
                    continue;
                }
                
                var humanResult = computer.FiredAt(coordinates);
                foreach (var outputLine in InputOutputMapper.MapOutputToStrings(humanResult))
                    Console.WriteLine("Human turn: {0}", outputLine);

                if (humanResult.GameOver)
                {
                    Console.ReadLine();
                    return;
                }

                var computerResult = human.FiredAt(new Coordinates(random.Next(10), random.Next(10)));

                foreach (var outputLine in InputOutputMapper.MapOutputToStrings(computerResult))
                    Console.WriteLine("Computer turn: {0}", outputLine);

                if (!computerResult.GameOver) continue;

                Console.ReadLine();
                return;
            }
        }
    }
}
