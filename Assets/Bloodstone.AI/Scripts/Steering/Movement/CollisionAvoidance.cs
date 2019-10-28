using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public class CollisionAvoidance : LocalAwarenessMovement
    {
        public float Radius = 0.2f;

        public override Vector3 GetSteering()
        {
            if (Neighborhood.Count == 0)
            {
                return Vector2.zero;
            }

            float maxPredictionDistance = Radius * 1.5f;

            float timeToCollision = float.MaxValue;
            Agent target = null;

            foreach (var a in Neighborhood)
            {
                var relativePos = (Agent.Position - a.Position);
                if (relativePos.sqrMagnitude > maxPredictionDistance * maxPredictionDistance)
                {
                    continue;
                }

                var relativeVelocity = (Agent.Velocity - a.Velocity);
                var estimatedTime = -Vector2.Dot(relativePos, relativeVelocity) / relativeVelocity.sqrMagnitude;

                if (estimatedTime > 0 && estimatedTime < timeToCollision)
                {
                    target = a;
                    timeToCollision = estimatedTime;
                }
            }

            if (target == null)
            {
                return Vector2.zero;
            }

            Vector2 result;

            if (timeToCollision <= 0 || (Agent.Position - target.Position).sqrMagnitude < Radius * Radius)
            {
                result = Agent.Position - target.Position;
            }
            else
            {
                var pos = Agent.Position - target.Position;
                var vel = Agent.Velocity - target.Velocity;

                result = pos + vel * timeToCollision;
            }

            return result;
        }
    }
}