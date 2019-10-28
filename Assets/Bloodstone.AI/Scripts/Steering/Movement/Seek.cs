using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public class Seek : MovementSteering
    {
        public Vector3 TargetPosition;

        public override Vector3 GetSteering()
        {
            return (TargetPosition - Agent.Position).normalized * Agent.Statistics.MaximumSpeed;
        }

        protected override void DrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(TargetPosition, 0.1f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(Agent.Position, TargetPosition);
        }
    }
}