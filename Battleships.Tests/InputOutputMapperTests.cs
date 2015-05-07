using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Battleships.Tests
{
    [TestClass]
    public class InputOutputMapperTests
    {
        [TestMethod]
        public void ShouldHandleInvalidInput()
        {
            Coordinates coordinates;
            const int arraySize = 10;

            Assert.IsFalse(InputOutputMapper.MapInputToArrayCoordinates("A0", out coordinates, arraySize), "Outside array bounds");
            Assert.IsFalse(InputOutputMapper.MapInputToArrayCoordinates("A11", out coordinates, arraySize), "Outside array bounds");
            Assert.IsFalse(InputOutputMapper.MapInputToArrayCoordinates("K1", out coordinates, arraySize), "Outside array bounds");
            Assert.IsFalse(InputOutputMapper.MapInputToArrayCoordinates("A", out coordinates, arraySize), "Too short");
            Assert.IsFalse(InputOutputMapper.MapInputToArrayCoordinates("AA", out coordinates, arraySize), "Second char should be a number");
        }

        [TestMethod]
        public void ShouldHandleValidInput()
        {
            Coordinates coordinates;
            const int arraySize = 10;

            Assert.IsTrue(InputOutputMapper.MapInputToArrayCoordinates("A1", out coordinates, arraySize));
            Assert.AreEqual(0, coordinates.X);
            Assert.AreEqual(0, coordinates.Y);

            Assert.IsTrue(InputOutputMapper.MapInputToArrayCoordinates("J10", out coordinates, arraySize));
            Assert.AreEqual(9, coordinates.X);
            Assert.AreEqual(9, coordinates.Y);

            Assert.IsTrue(InputOutputMapper.MapInputToArrayCoordinates("J5", out coordinates, arraySize));
            Assert.AreEqual(9, coordinates.X);
            Assert.AreEqual(4, coordinates.Y);
        }
    }
}
