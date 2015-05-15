using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Battleships.Tests
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void ShouldCalculateMaximumPossibleStartingCoordinates()
        {
            var grid = new Grid(10);
            var verticalResult = grid.GetXAndYMax(LineType.Vertical, 5);
            Assert.AreEqual(10, verticalResult.X);
            Assert.AreEqual(6, verticalResult.Y);

            var horizontalResult = grid.GetXAndYMax(LineType.Horizontal, 5);
            Assert.AreEqual(6, horizontalResult.X);
            Assert.AreEqual(10, horizontalResult.Y);
        }

        [TestMethod]
        public void ShouldGetVerticalLineOfSquares()
        {
            var grid = new Grid(10);

            grid.Squares[0, 2] = new Square {Contents = SquareContents.Ship};
            var verticalLine = grid.GetLineOfSquares(new Coordinates(0, 0), LineType.Vertical, 3);
            CollectionAssert.AreEqual(
                new[] {SquareContents.Sea, SquareContents.Sea, SquareContents.Ship},
                verticalLine.Select(x => x.Contents).ToArray());
        }

        [TestMethod]
        public void ShouldGetHorizontalLineOfSquares()
        {
            var grid = new Grid(10);

            grid.Squares[2, 0] = new Square { Contents = SquareContents.Ship };
            var horizontalLine = grid.GetLineOfSquares(new Coordinates(0, 0), LineType.Horizontal, 3);
            CollectionAssert.AreEqual(
                new[] { SquareContents.Sea, SquareContents.Sea, SquareContents.Ship },
                horizontalLine.Select(x => x.Contents).ToArray());
        }
    }
}
