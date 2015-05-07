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
            var verticalResult = grid.GetXAndYMax(true, 5);
            Assert.AreEqual(10, verticalResult.X);
            Assert.AreEqual(6, verticalResult.Y);

            var horizontalResult = grid.GetXAndYMax(false, 5);
            Assert.AreEqual(6, horizontalResult.X);
            Assert.AreEqual(10, horizontalResult.Y);
        }

        [TestMethod]
        public void ShouldGetLineOfSquares()
        {
            //skipped this for now
        }
    }
}
