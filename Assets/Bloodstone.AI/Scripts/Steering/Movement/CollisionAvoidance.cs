using UnityEngine;

namespace Bloodstone.AI.Steering.Movement
{
    public class CollisionAvoidance : LocalAwarenessMovement
    {
        public override Vector3 GetSteering()
        {
            if (Neighbourhood.Count == 0
                || !TryPredictCollision(out var collisionAgent, out var timeToCollision))
            {
                return Vector3.zero;
            }

            return CalculateAvoidanceForce(collisionAgent, timeToCollision);
        }

        private Vector3 CalculateAvoidanceForce(Agent collisionAgent, float timeToCollision)
        {
            var relativePosition = Agent.Position - collisionAgent.Position;

            if (timeToCollision > 0
                || relativePosition.sqrMagnitude > Agent.Radius * Agent.Radius)
            {
                var relativeVelocity = Agent.Velocity - collisionAgent.Velocity;

                return relativePosition + relativeVelocity * timeToCollision;
            }
            return relativePosition;
        }

        private bool TryPredictCollision(out Agent collisionAgent, out float timeToCollision)
        {
            timeToCollision = float.MaxValue;
            collisionAgent = null;

            float maxPredictionDistance = Agent.Radius + Agent.PredictionRange;
            foreach (var a in Neighbourhood)
            {
                var relativePos = Agent.Position - a.Position;
                if (relativePos.sqrMagnitude > maxPredictionDistance * maxPredictionDistance)
                {
                    continue;
                }

                var relativeVelocity = Agent.Velocity - a.Velocity;
                var estimatedTime = -Vector3.Dot(relativePos, relativeVelocity) / relativeVelocity.sqrMagnitude;

                if (estimatedTime > 0 && estimatedTime < timeToCollision)
                {
                    collisionAgent = a;
                    timeToCollision = estimatedTime;
                }
            }

            return collisionAgent != null;
        }
    }
}