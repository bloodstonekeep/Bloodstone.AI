using System.Collections;
using UnityEngine;

namespace Bloodstone
{
    public static class Vector2Extensions
    {
        public static Vector3 ToVector3(this Vector2 lhs)
        {
            return new Vector3(lhs.x, lhs.y);
        }
    }
}