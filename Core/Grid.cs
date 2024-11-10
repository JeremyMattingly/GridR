using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivTrak.Assessment.GridR.Core
{
    public class Grid
    {
        private readonly int[,] _sourceArray;
        private readonly Coordinate _maxCoordinate;

        public Dictionary<Coordinate, Cell> Cells { get; } = [];
        public int Threshold { get; }
        public Dictionary<uint, Subregion> Subregions { get; } = [];

        public Grid(int[,] sourceArray, int threshold)
        {
            if (sourceArray.Length == 0) throw new ArgumentException($"{nameof(sourceArray)} cannot be empty.");
            if (sourceArray.GetLength(0) != sourceArray.GetLength(1)) throw new ArgumentException($"{nameof(sourceArray)} dimension lengths must match.");

            _sourceArray = sourceArray;

            _maxCoordinate = new Coordinate((uint)_sourceArray.GetLength(0), (uint)_sourceArray.GetLength(1));

            Threshold = threshold;

            LoadCells();

            FindSubRegions();
        }

        private void LoadCells()
        {
            for (uint x = 0; x < _sourceArray.GetLength(0); x++)
            {
                for (uint y = 0; y < _sourceArray.GetLength(1); y++)
                {
                    Coordinate coordinate = new Coordinate(x, y);
                    Cells.Add(coordinate, new Cell(coordinate, _sourceArray[x, y]));
                }
            }
        }

        private void FindSubRegions()
        {
            Dictionary<Coordinate, Cell> interestingCells = GetInterestingCells();

            foreach (var interestingCell in interestingCells)
            {
                if (Subregions.Count == 0)
                {
                    CreatFirstSubRegionAndAddCellToIt(interestingCell);
                }
                else
                {
                    //See if a neighbor is accounted for
                    foreach (var neighbor in GetCellNeighbors(interestingCell.Value))
                    {
                        if (interestingCells.ContainsKey(neighbor))
                        {
                            //See if the interesting neighbor is accounted for in a subregion yet
                            var neighborAccountedForInSubregions = CellAccountedForInSubregions(neighbor);
                            if (neighborAccountedForInSubregions.Item1)
                            {
                                // add interesting cell to region containing neighbor
                                neighborAccountedForInSubregions.Item2.Cells.Add(interestingCell.Key, interestingCell.Value);
                                break;
                            }
                            else
                            {
                                //Look for neighbor's neighbors. if one of those are accounted for in regions, use that region
                                var neighborsneighbors = GetCellNeighbors(Cells[neighbor]);
                                var cellAddedViaNeighborsNeighbor = false;
                                foreach (var neighborsneighbor in neighborsneighbors)
                                {
                                    if (neighborsneighbor != interestingCell.Key) //Don't check yourself - endless loop
                                    {
                                        if (interestingCells.ContainsKey(neighborsneighbor))
                                        {
                                            //neighbor's neighbor is interesting
                                            var neighborsneighborAccountedForInSubregions = CellAccountedForInSubregions(neighborsneighbor);
                                            if (neighborsneighborAccountedForInSubregions.Item1)
                                            {
                                                //neighbor's neighbor is accounted for in a region so add interesting cell to that neighbor's neighbor's subregion
                                                neighborsneighborAccountedForInSubregions.Item2.Cells.Add(interestingCell.Key, interestingCell.Value);
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

                                //look until none of a neighbor's neighbors are in interesting list

                                //else, add new region

                            }
                        }
                    }
                }
            }
        }

        private void AddCellToNewSubregion(KeyValuePair<Coordinate, Cell> interestingCell)
        {
            Subregion newSubregion = new((uint)Subregions.Count);
            newSubregion.Cells.Add(interestingCell.Key, interestingCell.Value);
            Subregions.Add(newSubregion.Id, newSubregion);
        }

        private void CreatFirstSubRegionAndAddCellToIt(KeyValuePair<Coordinate, Cell> interestingCell)
        {
            Subregions.Add(0, new Subregion(0));
            Subregions[0].Cells.Add(interestingCell.Key, interestingCell.Value);
        }

        private Dictionary<Coordinate, Cell> GetInterestingCells()
        {
            Dictionary<Coordinate, Cell> interestingCells = [];

            foreach (var cell in Cells)
            {
                if (cell.Value.Signal >= Threshold)
                {
                    interestingCells.Add(cell.Key, cell.Value);
                }
            }

            return interestingCells;
        }

        private Tuple<bool, Subregion> CellAccountedForInSubregions(Coordinate interestingCell)
        {
            Tuple<bool, Subregion> returnValue = new Tuple<bool, Subregion>(false, null);

            foreach (var subregion in Subregions)
            {
                if (subregion.Value.Cells.ContainsKey(interestingCell))
                {
                    returnValue = new Tuple<bool, Subregion>(true, subregion.Value);
                    break;
                }
            }

            return returnValue;
        }

        private List<Coordinate> GetCellNeighbors(Cell cell)
        {
            List<Coordinate> neighbors = [];

            Coordinate topMiddle, topRight, right, bottomRight, bottomMiddle, bottomLeft, left, topLeft;

            //topMiddle
            if (cell.GridCoordinate.Y < _maxCoordinate.Y)
            {
                neighbors.Add(new Coordinate(cell.GridCoordinate.X, cell.GridCoordinate.Y + 1));

            }

            //topRight
            if (cell.GridCoordinate.X < _maxCoordinate.X && cell.GridCoordinate.Y < _maxCoordinate.Y)
            {
                neighbors.Add(new Coordinate(cell.GridCoordinate.X + 1, cell.GridCoordinate.Y + 1));
            }

            //right
            if (cell.GridCoordinate.X < _maxCoordinate.X)
            {
                neighbors.Add(new Coordinate(cell.GridCoordinate.X + 1, cell.GridCoordinate.Y));
            }

            //bottomRight
            if (cell.GridCoordinate.Y > 0 && cell.GridCoordinate.X < _maxCoordinate.X)
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
            if (cell.GridCoordinate.X > 0 && cell.GridCoordinate.Y < _maxCoordinate.Y)
            {
                neighbors.Add(new Coordinate(cell.GridCoordinate.X - 1, cell.GridCoordinate.Y + 1));
            }

            return neighbors;
        }
    }
}
