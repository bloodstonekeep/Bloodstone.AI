using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public class VelocityMatch : LocalAwarenessMovement
    {
        public override Vector3 GetSteering()
        {
            if(Neighborhood.Count == 0)
            {
                return Vector3.zero;
            }

            Vector3 netForce = Vector3.zero;

            foreach (var agent in Neighborhood)
            {
                netForce += agent.Velocity;
            }

            return netForce / Neighborhood.Count;
        }
    }
}