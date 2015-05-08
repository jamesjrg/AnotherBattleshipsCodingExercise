using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Battleships.Tests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void ShouldInitializePlayer()
        {
            var player = new Player(10, new Dictionary<int, int> {{3, 2}}, true);
            var squares = player.Grid.Squares;

            Assert.AreEqual(100, squares.Length);
            Assert.AreEqual(94, squares.Cast<Square>().Count(x => x.Contents == SquareContents.Sea));
            Assert.AreEqual(6, squares.Cast<Square>().Count(x => x.Contents == SquareContents.Ship));
        }

        [TestMethod]
        public void ShouldHandleFireAndMiss()
        {
            var player = new Player(10, new Dictionary<int, int> { { 3, 2 } }, false);
            player.ManuallyPlaceShip(new Coordinates(0, 0), false, 0);
            var squares = player.Grid.Squares;

            var result = player.FiredAt(new Coordinates(0, 1));
            Assert.AreEqual(SquareContents.Sea, result.SquareContents);
            Assert.AreEqual(false, result.ShipWasSunk);
            Assert.AreEqual(false, result.GameOver);
            Assert.AreEqual(97, squares.Cast<Square>().Count(x => x.Contents == SquareContents.Sea));
            Assert.AreEqual(3, squares.Cast<Square>().Count(x => x.Contents == SquareContents.Ship));
        }

        [TestMethod]
        public void ShouldHandleFireAndHit()
        {
            var player = new Player(10, new Dictionary<int, int> { { 3, 2 } }, false);
            player.ManuallyPlaceShip(new Coordinates(0, 0), false, 0);
            player.ManuallyPlaceShip(new Coordinates(0, 1), false, 1);
            var squares = player.Grid.Squares;

            var result = player.FiredAt(new Coordinates(0, 0));
            Assert.AreEqual(SquareContents.Ship, result.SquareContents);
            Assert.AreEqual(false, result.ShipWasSunk);
            Assert.AreEqual(false, result.GameOver);
            Assert.AreEqual(94, squares.Cast<Square>().Count(x => x.Contents == SquareContents.Sea));
            Assert.AreEqual(5, squares.Cast<Square>().Count(x => x.Contents == SquareContents.Ship));
            Assert.AreEqual(1, squares.Cast<Square>().Count(x => x.Contents == SquareContents.DamagedShip));
        }

        [TestMethod]
        public void ShouldHandleFireAndRepeatedlyHitDamagedShip()
        {
            var player = new Player(10, new Dictionary<int, int> { { 3, 2 } }, false);
            player.ManuallyPlaceShip(new Coordinates(0, 0), false, 0);
            player.ManuallyPlaceShip(new Coordinates(0, 1), false, 1);
            var squares = player.Grid.Squares;

            player.FiredAt(new Coordinates(0, 0));
            for (var i = 0; i != 10; ++i)
            {
                var result = player.FiredAt(new Coordinates(0, 0));
                Assert.AreEqual(SquareContents.DamagedShip, result.SquareContents);
                Assert.AreEqual(false, result.ShipWasSunk);
                Assert.AreEqual(false, result.GameOver);
                Assert.AreEqual(94, squares.Cast<Square>().Count(x => x.Contents == SquareContents.Sea));
                Assert.AreEqual(5, squares.Cast<Square>().Count(x => x.Contents == SquareContents.Ship));
                Assert.AreEqual(1, squares.Cast<Square>().Count(x => x.Contents == SquareContents.DamagedShip));
            }
        }

        [TestMethod]
        public void ShouldHandleFireAndShipSunk()
        {
            var player = new Player(10, new Dictionary<int, int> { { 3, 2 } }, false);
            player.ManuallyPlaceShip(new Coordinates(0, 0), false, 0);
            player.ManuallyPlaceShip(new Coordinates(0, 1), false, 1);
            var squares = player.Grid.Squares;

            player.FiredAt(new Coordinates(0, 0));
            player.FiredAt(new Coordinates(1, 0));
            var result = player.FiredAt(new Coordinates(2, 0));
            Assert.AreEqual(SquareContents.Ship, result.SquareContents);
            Assert.AreEqual(true, result.ShipWasSunk);
            Assert.AreEqual(false, result.GameOver);
            Assert.AreEqual(94, squares.Cast<Square>().Count(x => x.Contents == SquareContents.Sea));
            Assert.AreEqual(3, squares.Cast<Square>().Count(x => x.Contents == SquareContents.Ship));
            Assert.AreEqual(3, squares.Cast<Square>().Count(x => x.Contents == SquareContents.DamagedShip));
        }

        [TestMethod]
        public void ShouldHandleFireAndGameover()
        {
            var player = new Player(10, new Dictionary<int, int> { { 3, 2 } }, false);
            player.ManuallyPlaceShip(new Coordinates(0, 0), false, 0);
            player.ManuallyPlaceShip(new Coordinates(0, 1), false, 1);
            var squares = player.Grid.Squares;

            player.FiredAt(new Coordinates(0, 0));
            player.FiredAt(new Coordinates(1, 0));
            player.FiredAt(new Coordinates(2, 0));
            player.FiredAt(new Coordinates(0, 1));
            player.FiredAt(new Coordinates(1, 1));
            var result = player.FiredAt(new Coordinates(2, 1));
            Assert.AreEqual(SquareContents.Ship, result.SquareContents);
            Assert.AreEqual(true, result.ShipWasSunk);
            Assert.AreEqual(true, result.GameOver);
            Assert.AreEqual(94, squares.Cast<Square>().Count(x => x.Contents == SquareContents.Sea));
            Assert.AreEqual(0, squares.Cast<Square>().Count(x => x.Contents == SquareContents.Ship));
            Assert.AreEqual(6, squares.Cast<Square>().Count(x => x.Contents == SquareContents.DamagedShip));
        }
    }
}