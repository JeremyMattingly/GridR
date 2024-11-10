using System.Collections.ObjectModel;

namespace ActivTrak.Assessment.GridR.Core
{
    public class Subregion
    {
        private readonly Dictionary<Coordinate, Cell> _cells = [];

        public uint Id { get; }
        public ReadOnlyDictionary<Coordinate, Cell> Cells { get; private set; }

        private Subregion(uint id)
        {
            Id = id;
            Cells = _cells.AsReadOnly();
        }

        internal static Dictionary<uint, Subregion> GetSubregionsForGrid(Grid grid)
        {
            Dictionary<uint, Subregion> subregionsToReturn = [];

            Dictionary<Coordinate, Cell> interestingCells = GetInterestingCells();

            foreach (var interestingCell in interestingCells)
            {
                if (subregionsToReturn.Count == 0)
                {
                    CreatFirstSubRegionAndAddCellToIt(interestingCell);
                }
                else
                {
                    DetermineTargetSubregionAndAddCellToIt(interestingCells, interestingCell);
                }
            }

            return subregionsToReturn;

            Dictionary<Coordinate, Cell> GetInterestingCells()
            {
                Dictionary<Coordinate, Cell> interestingCells = [];

                foreach (var cell in grid.Cells)
                {
                    if (cell.Value.Signal >= grid.Threshold)
                    {
                        interestingCells.Add(cell.Key, cell.Value);
                    }
                }

                return interestingCells;
            }

            void CreatFirstSubRegionAndAddCellToIt(KeyValuePair<Coordinate, Cell> interestingCell)
            {
                subregionsToReturn.Add(0, new Subregion(0));
                subregionsToReturn[0].AddCell(interestingCell.Key, interestingCell.Value);
            }

            void DetermineTargetSubregionAndAddCellToIt(Dictionary<Coordinate, Cell> interestingCells, KeyValuePair<Coordinate, Cell> interestingCell)
            {
                //See if a neighbor is accounted for
                foreach (var neighbor in GetCellNeighbors(interestingCell.Value))
                {
                    if (interestingCells.ContainsKey(neighbor))
                    {
                        //See if the interesting neighbor is accounted for in a subregion yet
                        var neighborsSubregion = CellFoundInSubregion(neighbor);
                        if (neighborsSubregion is not null)
                        {
                            // add interesting cell to region containing neighbor
                            neighborsSubregion.AddCell(interestingCell.Key, interestingCell.Value);
                            break;
                        }
                        else
                        {
                            //Look for neighbor's neighbors. if one of those are accounted for in regions, use that region
                            var neighborsNeighbors = GetCellNeighbors(grid.Cells[neighbor]);
                            var cellAddedViaNeighborsNeighbor = false;
                            foreach (var neighborsNeighbor in neighborsNeighbors)
                            {
                                if (neighborsNeighbor != interestingCell.Key) //Don't check yourself - endless loop
                                {
                                    if (interestingCells.ContainsKey(neighborsNeighbor))
                                    {
                                        //neighbor's neighbor is interesting
                                        var neighborsNeighborsSubregion = CellFoundInSubregion(neighborsNeighbor);
                                        if (neighborsNeighborsSubregion is not null)
                                        {
                                            //neighbor's neighbor is accounted for in a region so add interesting cell to that neighbor's neighbor's subregion
                                            neighborsNeighborsSubregion.AddCell(interestingCell.Key, interestingCell.Value);
                                            cellAddedViaNeighborsNeighbor = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (cellAddedViaNeighborsNeighbor)
                            {
                                //cell has been added, so stop everything and move on to the next interesting cell
                                break;
                            }
                            else
                            {
                                // at this point, no neighbor's neighbors that weren't yourself were interesting or if interesting were already in a subregion. So create the new subregion and interesting cell to it
                                AddCellToNewSubregion(interestingCell);
                                break;
                            }
                        }
                    }
                }
            }

            List<Coordinate> GetCellNeighbors(Cell cell)
            {
                List<Coordinate> neighbors = [];

                //topMiddle
                if (cell.GridCoordinate.Y < grid.MaxCoordinate.Y)
                {
                    neighbors.Add(new Coordinate(cell.GridCoordinate.X, cell.GridCoordinate.Y + 1));
                }

                //topRight
                if (cell.GridCoordinate.X < grid.MaxCoordinate.X && cell.GridCoordinate.Y < grid.MaxCoordinate.Y)
                {
                    neighbors.Add(new Coordinate(cell.GridCoordinate.X + 1, cell.GridCoordinate.Y + 1));
                }

                //right
                if (cell.GridCoordinate.X < grid.MaxCoordinate.X)
                {
                    neighbors.Add(new Coordinate(cell.GridCoordinate.X + 1, cell.GridCoordinate.Y));
                }

                //bottomRight
                if (cell.GridCoordinate.Y > 0 && cell.GridCoordinate.X < grid.MaxCoordinate.X)
                {
                    neighbors.Add(new(cell.GridCoordinate.X + 1, cell.GridCoordinate.Y - 1));
                }

                //bottomMiddle
                if (cell.GridCoordinate.Y > 0)
                {
                    neighbors.Add(new Coordinate(cell.GridCoordinate.X, cell.GridCoordinate.Y - 1));
                }

                //bottomLeft
                if (cell.GridCoordinate.X > 0 && cell.GridCoordinate.Y > 0)
                {
                    neighbors.Add(new Coordinate(cell.GridCoordinate.X - 1, cell.GridCoordinate.Y - 1));
                }

                //left
                if (cell.GridCoordinate.X > 0)
                {
                    neighbors.Add(new Coordinate(cell.GridCoordinate.X - 1, cell.GridCoordinate.Y));
                }

                //topLeft
                if (cell.GridCoordinate.X > 0 && cell.GridCoordinate.Y < grid.MaxCoordinate.Y)
                {
                    neighbors.Add(new Coordinate(cell.GridCoordinate.X - 1, cell.GridCoordinate.Y + 1));
                }

                return neighbors;
            }

            Subregion? CellFoundInSubregion(Coordinate interestingCell)
            {
                Subregion? returnValue = null;

                foreach (var subregion in subregionsToReturn)
                {
                    if (subregion.Value.Cells.ContainsKey(interestingCell))
                    {
                        returnValue = subregion.Value;
                        break;
                    }
                }

                return returnValue;
            }

            void AddCellToNewSubregion(KeyValuePair<Coordinate, Cell> interestingCell)
            {
                Subregion newSubregion = new((uint)subregionsToReturn.Count);
                newSubregion.AddCell(interestingCell.Key, interestingCell.Value);
                subregionsToReturn.Add(newSubregion.Id, newSubregion);
            }
        }

        private void AddCell(Coordinate coordinate, Cell cell)
        {
            _cells.Add(coordinate, cell);
        }

        public override string ToString()
        {
            return Id.ToString();
        }


    }
}
