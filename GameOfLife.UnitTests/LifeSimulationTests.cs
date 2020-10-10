using System;
using AutoFixture;
using GameOfLife.ConsoleApp;
using Moq;
using NUnit.Framework;

namespace GameOfLife.UnitTests
{
    [TestFixture]
    public class LifeSimulationTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(1, 1)]
        public void NumberOfLiveNeighbours_AllCellsAreFalse_ReturnsZero(int x, int y)
        {
            var coordinate = new Coordinate { X = x, Y = y };
            bool[,] cells = new bool[2, 2]
            {
                { false, false },
                { false, false }
            };

            var mockBoard = new Mock<IBoard>();
            mockBoard.SetupGet(b => b.Height).Returns(2);
            mockBoard.SetupGet(b => b.Width).Returns(2);
            
            mockBoard.SetupGet(b => b.Cells).Returns(cells);

            var simulation = new LifeSimulation(mockBoard.Object);
            var numberOfLiveNeighbours = simulation.NumberOfLiveNeighbours(coordinate);

            Assert.AreEqual(0, numberOfLiveNeighbours);
        }


        [Test]
        public void NumberOfLiveNeighbours_XAndYAreZero_AllCellsAreTrue_Returns3()
        {
            var coordinate = new Coordinate { X = 0, Y = 0 };
            var mockBoard = new Mock<IBoard>();
            mockBoard.SetupGet(b => b.Height).Returns(3);
            mockBoard.SetupGet(b => b.Width).Returns(3);
            mockBoard.Setup(b => b.IsCellAlive(It.IsAny<Coordinate>())).Returns(true);

            var simulation = new LifeSimulation(mockBoard.Object);
            var numberOfLiveNeighbours = simulation.NumberOfLiveNeighbours(coordinate);

            Assert.AreEqual(3, numberOfLiveNeighbours);
        }

        [Test]
        public void NumberOfLiveNeighbours_YequalsOneLessHeight_XequalsOneLessWidth_AllCellsAreTrue_ReturnsThree()
        {
            var coordinate = new Coordinate { X = 2, Y = 2 };
            var mockBoard = new Mock<IBoard>();
            mockBoard.SetupGet(b => b.Height).Returns(3);
            mockBoard.SetupGet(b => b.Width).Returns(3);
            mockBoard.Setup(b => b.IsCellAlive(It.IsAny<Coordinate>())).Returns(true);

            var simulation = new LifeSimulation(mockBoard.Object);
            var numberOfLiveNeighbours = simulation.NumberOfLiveNeighbours(coordinate);

            Assert.AreEqual(3, numberOfLiveNeighbours);
        }

        [Test]
        public void NumberOfLiveNeighbours_CoordinatesCenter_AllCellsAreTrue_ReturnsEight()
        {
            var coordinate = new Coordinate { X = 1, Y = 1 };
            var mockBoard = new Mock<IBoard>();
            mockBoard.SetupGet(b => b.Height).Returns(3);
            mockBoard.SetupGet(b => b.Width).Returns(3);
            mockBoard.Setup(b => b.IsCellAlive(It.IsAny<Coordinate>())).Returns(true);

            var simulation = new LifeSimulation(mockBoard.Object);
            var numberOfLiveNeighbours = simulation.NumberOfLiveNeighbours(coordinate);

            Assert.AreEqual(8, numberOfLiveNeighbours);
        }

        [Test]
        public void NumberOfLiveNeighbours_CoordinatesCenter_AllCellsAreFalse_ReturnsZero()
        {
            var coordinate = new Coordinate { X = 1, Y = 1 };
            bool[,] cells = new bool[3, 3]
            {
                { false, false, false },
                { false, false, false },
                { false, false, false }
            };

            var mockBoard = new Mock<IBoard>();
            mockBoard.SetupGet(b => b.Height).Returns(3);
            mockBoard.SetupGet(b => b.Width).Returns(3);
            mockBoard.SetupGet(b => b.Cells).Returns(cells);

            var simulation = new LifeSimulation(mockBoard.Object);
            var numberOfLiveNeighbours = simulation.NumberOfLiveNeighbours(coordinate);

            Assert.AreEqual(0, numberOfLiveNeighbours);
        }

        [Test]
        public void NumberOfLiveNeighbours_CoordinatesCenter_OneAdjacentCellsIsTrue_ReturnsOne()
        {
            var coordinate = new Coordinate { X = 1, Y = 1 };
            var mockBoard = new Mock<IBoard>();
            mockBoard.SetupGet(b => b.Height).Returns(3);
            mockBoard.SetupGet(b => b.Width).Returns(3);
            mockBoard.SetupSequence(b => b.IsCellAlive(It.IsAny<Coordinate>()))
                .Returns(false)
                .Returns(false)
                .Returns(false)
                .Returns(false)
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(false)
                .Returns(false);

            var simulation = new LifeSimulation(mockBoard.Object);
            var numberOfLiveNeighbours = simulation.NumberOfLiveNeighbours(coordinate);

            Assert.AreEqual(1, numberOfLiveNeighbours);
        }

        [Test]
        public void ShouldCellTransitionToAlive_WhenCellIsAlive_NumberOfLiveNeighboursIsLessThan2_ReturnsTrue()
        {
            var coordinate = new Coordinate { X = _fixture.Create<int>(), Y = _fixture.Create<int>() };
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(b => b.IsCellAlive(It.IsAny<Coordinate>())).Returns(true);

            var simulation = new LifeSimulation(mockBoard.Object);

            var result = simulation.ShouldCellTransitionToAlive(coordinate, 1);

            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldCellTransitionToAlive_WhenCellIsAlive_NumberOfLiveNeighboursIsGreaterThan3_ReturnsTrue()
        {
            var coordinate = new Coordinate { X = _fixture.Create<int>(), Y = _fixture.Create<int>() };
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(b => b.IsCellAlive(It.IsAny<Coordinate>())).Returns(true);

            var simulation = new LifeSimulation(mockBoard.Object);

            var result = simulation.ShouldCellTransitionToAlive(coordinate, 4);

            Assert.IsTrue(result);
        }

        [Test]
        [TestCase(2)]
        [TestCase(3)]
        public void ShouldCellTransitionToAlive_WhenCellIsAlive_NumberOfLiveNeighboursEquals2or3_ReturnsFalse(int numberOfLiveNeighbours)
        {
            var coordinate = new Coordinate { X = _fixture.Create<int>(), Y = _fixture.Create<int>() };
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(b => b.IsCellAlive(It.IsAny<Coordinate>())).Returns(true);

            var simulation = new LifeSimulation(mockBoard.Object);

            var result = simulation.ShouldCellTransitionToAlive(coordinate, numberOfLiveNeighbours);

            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldCellTransitionToAlive_WhenCellIsDead_ReturnsFalse()
        {
            var coordinate = new Coordinate { X = _fixture.Create<int>(), Y = _fixture.Create<int>() };
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(b => b.IsCellAlive(It.IsAny<Coordinate>())).Returns(false);

            var simulation = new LifeSimulation(mockBoard.Object);

            var result = simulation.ShouldCellTransitionToAlive(coordinate, _fixture.Create<int>());

            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldCellTransitionToDead_WhenCellIsDead_NumberOfLiveNeighboursEquals3_ReturnsTrue()
        {
            var coordinate = new Coordinate { X = _fixture.Create<int>(), Y = _fixture.Create<int>() };
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(b => b.IsCellAlive(It.IsAny<Coordinate>())).Returns(false);

            var simulation = new LifeSimulation(mockBoard.Object);

            var result = simulation.ShouldCellTransitionToDead(coordinate, 3);

            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldCellTransitionToDead_WhenCellIsDead_NumberOfLiveNeighboursIsLessThan3_ReturnsFalse()
        {
            var coordinate = new Coordinate { X = _fixture.Create<int>(), Y = _fixture.Create<int>() };
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(b => b.IsCellAlive(It.IsAny<Coordinate>())).Returns(false);

            var simulation = new LifeSimulation(mockBoard.Object);

            var result = simulation.ShouldCellTransitionToDead(coordinate, 2);

            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldCellTransitionToDead_WhenCellIsDead_NumberOfLiveNeighboursIsGreaterThan3_ReturnsFalse()
        {
            var coordinate = new Coordinate { X = _fixture.Create<int>(), Y = _fixture.Create<int>() };
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(b => b.IsCellAlive(It.IsAny<Coordinate>())).Returns(false);

            var simulation = new LifeSimulation(mockBoard.Object);

            var result = simulation.ShouldCellTransitionToDead(coordinate, 4);

            Assert.IsFalse(result);
        }

        [Test]
        public void GetCellSymbol_CellIsAlive_ReturnsLiveCellSymbol()
        {
            var coordinate = new Coordinate { X = _fixture.Create<int>(), Y = _fixture.Create<int>() };
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(b => b.IsCellAlive(It.IsAny<Coordinate>())).Returns(true);

            var simulation = new LifeSimulation(mockBoard.Object);

            var result = simulation.GetCellSymbol(coordinate);

            Assert.AreEqual(Constants.LiveCell, result);
        }

        [Test]
        public void GetCellSymbol_CellIsDead_ReturnsDeadCellSymbol()
        {
            var coordinate = new Coordinate { X = _fixture.Create<int>(), Y = _fixture.Create<int>() };
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(b => b.IsCellAlive(It.IsAny<Coordinate>())).Returns(false);

            var simulation = new LifeSimulation(mockBoard.Object);

            var result = simulation.GetCellSymbol(coordinate);

            Assert.AreEqual(Constants.DeadCell, result);
        }
    }
}
