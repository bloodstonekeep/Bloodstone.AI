using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public class HelloRotation : RotationSteering
    {
        public override Vector3 GetSteering()
        {
            return Vector3.one;
        }
    }
}