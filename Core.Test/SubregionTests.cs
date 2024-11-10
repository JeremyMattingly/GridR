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

        [TestMethod]
        public void GetSubregionsForGrid_ValidInput_ProperlyFindsSubregions()
        {
            var (exampleArray, threshold) = Grid.GetExampleArray();
            var expectedCellsForInput = ExpectedCellsForExampleArray.Cells;

            var expectedSubregions = new Dictionary<uint, Dictionary<Coordinate, Cell>>();

            var subregion1 = new Dictionary<Coordinate, Cell>
            {
                { new(1, 1), expectedCellsForInput[new(1, 1)] }
            };
            expectedSubregions.Add(0, subregion1);

            var subregion2 = new Dictionary<Coordinate, Cell>
            {
                { new(3, 3), expectedCellsForInput[new(3, 3)] },
                { new(3, 2), expectedCellsForInput[new(3, 2)] },
                { new(4, 2), expectedCellsForInput[new(4, 2)] },
                { new(4, 1), expectedCellsForInput[new(4, 1)] },
                { new(5, 1), expectedCellsForInput[new(5, 1)] }
            };
            expectedSubregions.Add(1, subregion2);

            var subregion3 = new Dictionary<Coordinate, Cell>
            {
                { new(5, 4), expectedCellsForInput[new Coordinate(5, 4)] },
                { new(5, 5), expectedCellsForInput[new Coordinate(5, 5)] }
            };
            expectedSubregions.Add(2, subregion3);

            var result = new Grid(exampleArray, threshold);

            Assert.IsTrue(result.Subregions.Count == 3);

            foreach (var expectedSubregion in expectedSubregions)
            {
                Assert.IsTrue(result.Subregions.ContainsKey(expectedSubregion.Key));

                Assert.AreEqual(expectedSubregion.Value.Count, result.Subregions[expectedSubregion.Key].Cells.Count);

                foreach (var expectedCell in expectedSubregion.Value.Values)
                {
                    Assert.IsTrue(result.Subregions[expectedSubregion.Key].Cells.ContainsKey(expectedCell.GridCoordinate)
                                  && result.Subregions[expectedSubregion.Key].Cells[expectedCell.GridCoordinate].Equals(expectedCell));
                }
            }
        }
    }
}
