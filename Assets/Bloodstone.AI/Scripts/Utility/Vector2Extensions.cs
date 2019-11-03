using UnityEngine;

namespace Bloodstone.AI.Utility
{
    public static class Vector2Extensions
    {
        public static Vector3 ToVector3(this Vector2 lhs)
        {
            return new Vector3(lhs.x, lhs.y);
        }

        public static Vector2 Rotate(this Vector2 lhs, float angleSin, float angleCos)
        {
            return new Vector2(
                x: angleCos * lhs.x - angleSin * lhs.y,
                y: angleSin * lhs.x + angleCos * lhs.y);
        }
    }
}