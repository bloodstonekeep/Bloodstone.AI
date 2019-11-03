using UnityEngine;

namespace Bloodstone.AI.Steering.Movement
{
    public class Cohesion : LocalAwarenessMovement
    {
        public override Vector3 GetSteering()
        {
            if (Neighbourhood.Count == 0)
            {
                return Vector3.zero;
            }

            Vector3 averagePosition = GetAverageNeightborsPosition();

            return averagePosition - Agent.Position;
        }

        private Vector3 GetAverageNeightborsPosition()
        {
            Vector3 averagePosition = Vector3.zero;

            foreach (var agent in Neighbourhood)
            {
                averagePosition += agent.Position;
            }

            return averagePosition / Neighbourhood.Count;
        }
    }
}