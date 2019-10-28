using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public class HelloMovement : MovementSteering
    {
        public override Vector3 GetSteering()
        {
            return Vector3.one;
        }
    }
}