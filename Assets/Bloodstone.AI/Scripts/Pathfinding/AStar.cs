using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bloodstone.AI.Pathfinding
{
    public class AStar : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int _cellSize;

        [SerializeField]
        private List<Transform> _worldTerrain;

        private WorldGrid _worldGrid;

        private void Awake()
        {
            _worldGrid = new WorldGrid(_worldTerrain.Select(t => t.position).ToList(), _cellSize);
        }

        public List<WorldCell> GetPath(Vector3 startPoint, Vector3 endPoint)
        {
            var startCoords = _worldGrid.GetCoordsByPosition(startPoint);
            var finishCoords = _worldGrid.GetCoordsByPosition(endPoint);

            var startCell = _worldGrid.Cells[startCoords.y, startCoords.x];
            var finishCell = _worldGrid.Cells[finishCoords.y, finishCoords.x];

            if (startCell == finishCell)
            {
                return new List<WorldCell> { finishCell };
            }

            var openList = new List<WorldCell>();
            var closedList = new List<WorldCell>();

            startCell.Heuristic = Vector2.Distance(startPoint, endPoint);
            startCell.Cost = 0;
            startCell.Parent = null;

            openList.Add(startCell);

            while (openList.Count > 0)
            {
                var bestCell = FindBestCell(openList);
                openList.Remove(bestCell);

                var bestCellCoords = _worldGrid.GetCoordsByPosition(bestCell.Position);
                var neighbours = _worldGrid.GetNearbyCells(bestCellCoords.x, bestCellCoords.y);

                for (int i = 0; i < neighbours.Count; ++i)
                {
                    var neighbour = neighbours[i];
                    if (neighbour == finishCell)
                    {
                        neighbour.Parent = bestCell;
                        return CreatePath(neighbour);
                    }

                    var cost = bestCell.Cost + (neighbour.Position - bestCell.Position).magnitude;
                    var heuristic = (finishCell.Position - neighbour.Position).magnitude;

                    if (openList.Contains(neighbour) && neighbour.Cost + neighbour.Heuristic < cost + heuristic
                        || closedList.Contains(neighbour) && neighbour.Cost + neighbour.Heuristic < cost + heuristic)
                    {
                        continue;
                    }

                    neighbour.Cost = cost;
                    neighbour.Heuristic = heuristic;
                    neighbour.Parent = bestCell;

                    if (!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }
                }

                if (!closedList.Contains(bestCell))
                {
                    closedList.Add(bestCell);
                }
            }

            return CreatePath(finishCell);
        }

        private static WorldCell FindBestCell(List<WorldCell> openList)
        {
            var bestCell = openList[0];
            var bestCost = bestCell.Cost * bestCell.Heuristic;

            for (var i = 1; i < openList.Count; ++i)
            {
                var cell = openList[i];
                var cellCost = cell.Cost + cell.Heuristic;
                if (bestCost > cellCost)
                {
                    bestCell = cell;
                    bestCost = cellCost;
                }
            }

            return bestCell;
        }

        private static List<WorldCell> CreatePath(WorldCell endCell)
        {
            var path = new List<WorldCell> { endCell };
            var curr = endCell;

            while (curr.Parent != null)
            {
                curr = curr.Parent;
                path.Add(curr);
            }

            path.Reverse();
            return path;
        }
    }
}