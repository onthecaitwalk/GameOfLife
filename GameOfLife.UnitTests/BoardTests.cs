using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using GameOfLife.ConsoleApp;
using NUnit.Framework;

namespace GameOfLife.UnitTests
{
    [TestFixture]
    public class BoardTests
    {

        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void HeightIsZero_CellsEmpty()
        {
            var board = new Board
            {
                Height = 0,
                Width = _fixture.Create<int>()
            };

            Assert.IsEmpty(board.Cells);
        }

        [Test]
        public void WidthIsZero_CellsEmpty()
        {
            var board = new Board
            {
                Height = _fixture.Create<int>(),
                Width = 0
            };

            Assert.IsEmpty(board.Cells);
        }

        [Test]
        public void HeightAndWidthAreGreaterThanZero_ReturnsCells_HeightIsFirstDimension_WidthIsSecondDimension()
        {
            var board = new Board
            {
                Height = 2,
                Width = 3
            };

            Assert.Multiple(() =>
            {
                Assert.AreEqual(2, board.Cells.GetLength(0));
                Assert.AreEqual(3, board.Cells.GetLength(1));
                Assert.AreEqual(6, board.Cells.Length);
            });
        }

        [Test]
        public void GenerateCells_HeightAndWidthAreGreaterThanZero_ReturnsCells_HeightIsFirstDimension_WidthIsSecondDimension_PopulatedWithBools()
        {
            var board = new Board
            {
                Height = 2,
                Width = 3
            };

            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<bool>(board.Cells[0, 0]);
                Assert.IsInstanceOf<bool>(board.Cells[0, 1]);
                Assert.IsInstanceOf<bool>(board.Cells[0, 2]);
                Assert.IsInstanceOf<bool>(board.Cells[1, 0]);
                Assert.IsInstanceOf<bool>(board.Cells[1, 1]);
                Assert.IsInstanceOf<bool>(board.Cells[1, 2]);
            });
        }
    }
}
