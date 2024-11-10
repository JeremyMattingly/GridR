using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivTrak.Assessment.GridR.Core.Test
{
    [TestClass]
    public class CellTests
    {
        [TestMethod]
        public void Constructor_ValidInput_ProperlyInstantiates()
        {
            var coordinate = new Coordinate(23, 45);
            var value = 234;

            var cell = new Cell(coordinate, value);

            Assert.AreEqual(coordinate, cell.GridCoordinate);
            Assert.AreEqual(value, cell.Signal);
        }

        [TestMethod]
        public void Equals_EqualCell_ReturnsTrue()
        {
            var cell1 = new Cell(new Coordinate(23, 45), 88);
            var cell2 = new Cell(new Coordinate(23, 45), 88);

            Assert.IsTrue(cell1.Equals(cell2));

        }

        [TestMethod]
        public void Equals_CoordinateNotEqual_ReturnsFalse()
        {
            var cell1 = new Cell(new Coordinate(23, 45), 88);
            var cell2 = new Cell(new Coordinate(45, 23), 88);

            Assert.IsFalse(cell1.Equals(cell2));

        }

        [TestMethod]
        public void Equals_SignalNotEqual_ReturnsFalse()
        {
            var cell1 = new Cell(new Coordinate(23, 45), 88);

            var cell2 = new Cell(new Coordinate(23, 45), -88);

            Assert.IsFalse(cell1.Equals(cell2));

        }
    }
}
