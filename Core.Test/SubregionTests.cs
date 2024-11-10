namespace ActivTrak.Assessment.GridR.Core.Test
{
    [TestClass]
    public class SubregionTests
    {
        [TestMethod]
        public void Constructor_ValidInput_ProperlyInstantiates()
        {
            uint input = 1;

            var result = new Subregion(input);

            Assert.AreEqual(input, result.Id);
            Assert.IsNotNull(result.Cells);
        }

        [TestMethod]
        public void Equals_EqualSubregion_ReturnsTrue()
        {
            var subregion1 = new Subregion(1);

            var subregion2 = new Subregion(1);

            Assert.IsTrue(subregion1.Equals(subregion2));
        }

        [TestMethod]
        public void Equals_SubregionNotEqual_ReturnsFalse()
        {
            var subregion1 = new Subregion(1);

            var subregion2 = new Subregion(2);

            Assert.IsFalse(subregion1.Equals(subregion2));
        }

        [TestMethod]
        public void OperatorEquals_DoesEqual_ReturnsTrue()
        {
            var subregionA = new Subregion(23);
            var subregionB = new Subregion(23);

            Assert.IsTrue(subregionA == subregionB);
        }

        [TestMethod]
        public void OperatorEquals_DoesNotEqual_ReturnsFalse()
        {
            var subregionA = new Subregion(23);
            var subregionB = new Subregion(45);

            Assert.IsFalse(subregionA == subregionB);
        }

        [TestMethod]
        public void OperatorNotEqual_DoesNotEqual_ReturnsTrue()
        {
            var subregionA = new Subregion(23);
            var subregionB = new Subregion(45);

            Assert.IsTrue(subregionA != subregionB);
        }

        [TestMethod]
        public void OperatorNotEqual_DoesEqual_ReturnsFalse()
        {
            var subregionA = new Subregion(23);
            var subregionB = new Subregion(23);

            Assert.IsFalse(subregionA != subregionB);
        }
    }
}
