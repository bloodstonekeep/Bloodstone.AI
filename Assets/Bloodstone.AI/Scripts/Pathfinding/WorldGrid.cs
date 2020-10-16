using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bloodstone.AI.Pathfinding
{
    public class WorldGrid
    {
        private readonly WorldCell[,] _cells;

        private int _width;
        private int _height;
        private Vector2Int _worldCellSize;
        private Vector2Int _cellOffset;

        public WorldGrid(List<Vector3> cellsPositions, Vector2Int cellSize)
        {
            _cells = ConstructCells(cellsPositions);

            _worldCellSize = cellSize;
            _cellOffset = _worldCellSize * 2;
        }

        public WorldCell[,] Cells => _cells;

        private WorldCell[,] ConstructCells(List<Vector3> cellsPositions)
        {
            _width = (from p in cellsPositions
                      select p.x)
                         .Distinct()
                         .Count();

            _height = (from p in cellsPositions
                       select p.z)
                         .Distinct()
                         .Count();

            var cell = new WorldCell[_height, _width];

            for (int y = 0; y < _height; ++y)
            {
                for (int x = 0; x < _height; ++x)
                {
                    cell[y, x] = new WorldCell(cellsPositions[y * _width + x]);
                }
            }

            return cell;
        }

        public (int x, int y) GetCoordsByPosition(Vector3 position)
        {
            var x = (int)position.x / _cellOffset.x;
            var y = (int)position.z / _cellOffset.y;
            var mX = (int)(Mathf.Abs(position.x) % _cellOffset.x);
            var mY = (int)(Mathf.Abs(position.z) % _cellOffset.y);

            if (mX > _worldCellSize.x)
            {
                x++;
            }

            if (mY > _worldCellSize.y)
            {
                y++;
            }

            return (x, y);
        }

        public List<WorldCell> GetNearbyCells(int targetX, int targetY)
        {
            var result = new List<WorldCell>();
            var nearbyCoords = GetNearbyCellsCoords(targetX, targetY);

            foreach (var (x, y) in nearbyCoords)
            {
                result.Add(_cells[y, x]);
            }

            return result;
        }

        public List<(int x, int y)> GetNearbyCellsCoords(int targetX, int targetY)
        {
            var neighbours = new List<(int x, int y)>();

            if (targetY < _height - 1)
            {
                int north = targetY + 1;
                neighbours.Add((x: targetX, y: north));
            }

            if (targetY > 0)
            {
                int south = targetY - 1;
                neighbours.Add((x: targetX, y: south));
            }

            if (targetX < _width - 1)
            {
                int east = targetX + 1;
                neighbours.Add((x: east, y: targetY));
            }

            if (targetX > 0)
            {
                int west = targetX - 1;
                neighbours.Add((x: west, y: targetY));
            }

            if (targetY < _height - 1 && targetX > 0)
            {
                int north = targetY + 1;
                int west = targetX - 1;

                neighbours.Add((x: west, y: north));
            }

            if (targetY < _height - 1 && targetX < _width - 1)
            {
                int north = targetY + 1;
                int east = targetX + 1;

                neighbours.Add((x: east, y: north));
            }

            if (targetY > 0 && targetX > 0)
            {
                int south = targetY - 1;
                int west = targetX - 1;

                neighbours.Add((x: west, y: south));
            }

            if (targetY > 0 && targetX < _width - 1)
            {
                int south = targetY - 1;
                int east = targetX + 1;

                neighbours.Add((x: east, y: south));
            }

            return neighbours;
        }
    }
}