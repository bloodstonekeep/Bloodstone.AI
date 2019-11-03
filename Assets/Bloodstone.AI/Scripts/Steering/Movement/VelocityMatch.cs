using UnityEngine;

namespace Bloodstone.AI.Steering.Movement
{
    public class VelocityMatch : LocalAwarenessMovement
    {
        public override Vector3 GetSteering()
        {
            if (Neighbourhood.Count == 0)
            {
                return Vector3.zero;
            }

            return CalculateAverageLocalVelocity();
        }

        private Vector3 CalculateAverageLocalVelocity()
        {
            Vector3 averageVelocity = Vector3.zero;

            foreach (var agent in Neighbourhood)
            {
                averageVelocity += agent.Velocity;
            }

            return averageVelocity /= Neighbourhood.Count;
        }
    }
}