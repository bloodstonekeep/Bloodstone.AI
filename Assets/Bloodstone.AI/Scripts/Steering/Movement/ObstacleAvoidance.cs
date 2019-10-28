using System;
using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public class ObstacleAvoidance : LocalAwarenessMovement
    {
        public float PerceptionRadius = 1f;

        [SerializeField]
        protected float _avoidance = .5f;

        public override Vector3 GetSteering()
        {
            // todo: move constraint: cos, sin
            const float angle = Mathf.PI / 3f;

            Vector3 result = Vector3.zero;
            var forward = Agent.Velocity;

            var cosl = Mathf.Cos(angle);
            var sinl = Mathf.Sin(angle);
            var leftDir = new Vector2(cosl * Agent.Velocity.x - sinl * Agent.Velocity.y, sinl * Agent.Velocity.x + cosl * Agent.Velocity.y);

            var cosr = Mathf.Cos(-angle);
            var sinr = Mathf.Sin(-angle);
            var rightDir = new Vector2(cosr * Agent.Velocity.x - sinr * Agent.Velocity.y, sinr * Agent.Velocity.x + cosr * Agent.Velocity.y);

            result += Raycast(forward);
            result += Raycast(leftDir);
            result += Raycast(rightDir);

            return result;
        }

        private Vector3 Raycast(Vector3 direction)
        {
            switch (Agent.WorldOrientation)
            {
                case WorldOrientation.XY:
                    {
                        return Raycast_XY(direction);
                    }
                default:
                    {
                        throw new NotImplementedException("Not yet implemented...");
                    }
            }
        }

        private Vector3 Raycast_XY(Vector3 direction)
        {
            //Debug.DrawLine(Agent.Position, Agent.Position + direction.normalized * PerceptionRadius, Color.yellow);
            var hit = Physics2D.Raycast(Agent.Position, direction.normalized, PerceptionRadius);
            if (hit.collider != null)
            {
                var targetPos = hit.point + hit.normal * _avoidance;
                return targetPos.ToVector3() - Agent.Position;
            }

            return Vector3.zero;
        }
    }
}