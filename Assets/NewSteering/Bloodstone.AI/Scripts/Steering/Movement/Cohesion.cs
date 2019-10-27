using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public class Cohesion : LocalAwarenessMovement
    {
        public override Vector3 GetSteering()
        {
            if (Neighborhood.Count == 0)
            {
                return Vector3.zero;
            }

            Vector3 averagePosition = Vector3.zero;

            foreach (var agent in Neighborhood)
            {
                averagePosition += agent.Position;
            }

            averagePosition /= Neighborhood.Count;

            var netForce = averagePosition - Agent.Position;
            //return netForce.normalized * Agent.Statistics.MaximumSpeed;
            return netForce;
        }
    }
}