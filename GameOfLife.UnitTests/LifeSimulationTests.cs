using GameOfLife.ConsoleApp;
using Moq;
using NUnit.Framework;

namespace GameOfLife.UnitTests
{
    [TestFixture]
    public class LifeSimulationTests
    {
        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(0, 2)]
        [TestCase(1, 0)]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 0)]
        [TestCase(2, 1)]
        [TestCase(2, 2)]
        public void NumberOfLiveNeighbours_AllCellsAreDead_ReturnsZero(int x, int y)
        {
            var coordinate = new Coordinate { X = x, Y = y };
            var cell = new Cell { IsAlive = false };
            var mockBoard = new Mock<IBoard>();
            mockBoard.SetupGet(b => b.Height).Returns(3);
            mockBoard.SetupGet(b => b.Width).Returns(3);
            mockBoard.Setup(b => b.GetCell(It.IsAny<Coordinate>())).Returns(cell);

            var simulation = new LifeSimulation(mockBoard.Object);
            var numberOfLiveNeighbours = simulation.NumberOfLiveNeighbours(coordinate);

            Assert.AreEqual(0, numberOfLiveNeighbours, "Should return zero live neighbours when all cells are dead.");
        }


        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 2)]
        [TestCase(2, 0)]
        [TestCase(2, 2)]
        public void NumberOfLiveNeighbours_CoordinatesAreCornersOfBoard_AllCellsAreAlive_Returns3(int x, int y)
        {
            var coordinate = new Coordinate { X = x, Y = y };
            var cell = new Cell { IsAlive = true };
            var mockBoard = new Mock<IBoard>();
            mockBoard.SetupGet(b => b.Height).Returns(3);
            mockBoard.SetupGet(b => b.Width).Returns(3);
            mockBoard.Setup(b => b.GetCell(It.IsAny<Coordinate>())).Returns(cell);

            var simulation = new LifeSimulation(mockBoard.Object);
            var numberOfLiveNeighbours = simulation.NumberOfLiveNeighbours(coordinate);

            Assert.AreEqual(3, numberOfLiveNeighbours, "Should return 3 live neighbours when all cells are live and coordinates are for corner cell of board.");
        }

        [Test]
        public void NumberOfLiveNeighbours_CoordinatesCenter_AllCellsAreAlive_ReturnsEight()
        {
            var coordinate = new Coordinate { X = 1, Y = 1 };
            var cell = new Cell { IsAlive = true };
            var mockBoard = new Mock<IBoard>();
            mockBoard.SetupGet(b => b.Height).Returns(3);
            mockBoard.SetupGet(b => b.Width).Returns(3);
            mockBoard.Setup(b => b.GetCell(It.IsAny<Coordinate>())).Returns(cell);

            var simulation = new LifeSimulation(mockBoard.Object);
            var numberOfLiveNeighbours = simulation.NumberOfLiveNeighbours(coordinate);

            Assert.AreEqual(8, numberOfLiveNeighbours, "Should return 8 live neighbours when all cells are live and coordinates are for a cell in the center of the board.");
        }

        [Test]
        public void NumberOfLiveNeighbours_CoordinatesCenter_OneAdjacentCellIsAlive_ReturnsOne()
        {
            var coordinate = new Coordinate { X = 1, Y = 1 };
            var aliveCell = new Cell { IsAlive = true };
            var deadCell = new Cell { IsAlive = false };
            var mockBoard = new Mock<IBoard>();
            mockBoard.SetupGet(b => b.Height).Returns(3);
            mockBoard.SetupGet(b => b.Width).Returns(3);
            mockBoard.SetupSequence(b => b.GetCell(It.IsAny<Coordinate>()))
                .Returns(deadCell)
                .Returns(deadCell)
                .Returns(deadCell)
                .Returns(deadCell)
                .Returns(deadCell)
                .Returns(aliveCell)
                .Returns(deadCell)
                .Returns(deadCell)
                .Returns(deadCell);

            var simulation = new LifeSimulation(mockBoard.Object);
            var numberOfLiveNeighbours = simulation.NumberOfLiveNeighbours(coordinate);

            Assert.AreEqual(1, numberOfLiveNeighbours, "Should return 1 live neighbours when only one adjacent cell is alive.");
        }
    }
}
