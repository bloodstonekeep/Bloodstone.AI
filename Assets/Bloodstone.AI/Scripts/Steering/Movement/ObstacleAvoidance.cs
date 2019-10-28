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
            const float angle = Mathf.PI / 3f;

            Vector3 result = Vector2.zero;

            Debug.DrawLine(Agent.Position, Agent.Position + Agent.Velocity.normalized * PerceptionRadius, Color.yellow);
            var hit = Physics2D.Raycast(Agent.Position, Agent.Velocity.normalized, PerceptionRadius);
            if (hit.collider != null)
            {
                var targetPos = hit.point + hit.normal * _avoidance;
                var t3 = new Vector3(targetPos.x, targetPos.y);
                result += t3 - Agent.Position;
            }

            var cosl = Mathf.Cos(angle);
            var sinl = Mathf.Sin(angle);
            var leftDir = new Vector2(cosl * Agent.Velocity.x - sinl * Agent.Velocity.y, sinl * Agent.Velocity.x + cosl * Agent.Velocity.y);

            Debug.DrawLine(Agent.Position, Agent.Position + leftDir.normalized.ToVector3() * PerceptionRadius, Color.yellow);

            hit = Physics2D.Raycast(Agent.Position, leftDir.normalized, PerceptionRadius);
            if (hit.collider != null)
            {
                var targetPos = hit.point + hit.normal * _avoidance;
                var t3 = new Vector3(targetPos.x, targetPos.y);
                result += t3 - Agent.Position;
            }

            var cosr = Mathf.Cos(-angle);
            var sinr = Mathf.Sin(-angle);
            var rightDir = new Vector2(cosr * Agent.Velocity.x - sinr * Agent.Velocity.y, sinr * Agent.Velocity.x + cosr * Agent.Velocity.y);

            Debug.DrawLine(Agent.Position, Agent.Position + rightDir.normalized.ToVector3() * PerceptionRadius, Color.yellow);

            hit = Physics2D.Raycast(Agent.Position, rightDir.normalized, PerceptionRadius);
            if (hit.collider != null)
            {
                var targetPos = hit.point + hit.normal * _avoidance;
                var t3 = new Vector3(targetPos.x, targetPos.y);
                result += t3 - Agent.Position;
            }

            return result;
        }
    }
}