namespace ActivTrak.Assessment.GridR.Core.Test
{
    [TestClass]
    public class CoordinateTests
    {
        [TestMethod]
        public void Constructor_ValidInput_ProperlyInstantiates()
        {
            uint x = 23;
            uint y = 45;

            var result = new Coordinate(x, y);

            Assert.AreEqual(x, result.X);
            Assert.AreEqual(y, result.Y);
        }

        [TestMethod]
        public void Equals_DoesEqual_ReturnsTrue()
        {
            uint x = 23;
            uint y = 45;

            var coordinateA = new Coordinate(x, y);
            var coordinateB = new Coordinate(x, y);

            Assert.IsTrue(coordinateA.Equals(coordinateB));
        }

        [TestMethod]
        public void Equals_DoesNotEqual_ReturnsFalse()
        {
            var coordinateA = new Coordinate(23, 45);
            var coordinateB = new Coordinate(45, 23);

            Assert.IsFalse(coordinateA.Equals(coordinateB));
        }

        [TestMethod]
        public void OperatorEquals_DoesEqual_ReturnsTrue()
        {
            var coordinateA = new Coordinate(23, 45);
            var coordinateB = new Coordinate(23, 45);

            Assert.IsTrue(coordinateA == coordinateB);
        }

        [TestMethod]
        public void OperatorEquals_DoesNotEqual_ReturnsFalse()
        {
            var coordinateA = new Coordinate(23, 45);
            var coordinateB = new Coordinate(45, 23);

            Assert.IsFalse(coordinateA == coordinateB);
        }

        [TestMethod]
        public void OperatorNotEqual_DoesNotEqual_ReturnsTrue()
        {
            var coordinateA = new Coordinate(23, 45);
            var coordinateB = new Coordinate(45, 23);

            Assert.IsTrue(coordinateA != coordinateB);
        }

        [TestMethod]
        public void OperatorNotEqual_DoesEqual_ReturnsFalse()
        {
            var coordinateA = new Coordinate(23, 45);
            var coordinateB = new Coordinate(23, 45);

            Assert.IsFalse(coordinateA != coordinateB);
        }
    }
}
