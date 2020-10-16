using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bloodstone.AI.Pathfinding
{
    [Serializable]
    public class WorldCell
    {
        public WorldCell(Vector3 position)
        {
            Position = position;

            var leftTopPos = position + new Vector3(-1, 0, 1) * 2.5f;
            var rightTopPos = position + new Vector3(1, 0, 1) * 2.5f;
            var leftBotPos = position + new Vector3(-1, 0, -1) * 2.5f;
            var rightBotPos = position + new Vector3(1, 0, -1) * 2.5f;

            Subcells = new List<WorldSubCell>
                {
                    new WorldSubCell(leftTopPos),
                    new WorldSubCell(leftBotPos),
                    new WorldSubCell(rightTopPos),
                    new WorldSubCell(rightBotPos)
                };
        }

        public float Cost;
        public float Weight;
        public bool Occupied;
        public float Heuristic;
        public WorldCell Parent;
        public Vector3 Position;
        public List<WorldSubCell> Subcells;
    }

    public class WorldSubCell
    {
        public WorldSubCell(Vector3 position)
        {
            Position = position;
        }

        public Vector3 Position;
        public bool Occupied;
    }
}