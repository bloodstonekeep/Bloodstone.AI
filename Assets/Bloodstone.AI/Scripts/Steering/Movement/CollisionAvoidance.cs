using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public class CollisionAvoidance : MovementSteering
    {
        [SerializeField]
        private float _perceptionRange;
        [SerializeField]
        private float _perceptionAngle;

        public override Vector3 GetSteering()
        {
            Vector3 forward;
            switch (Agent.WorldOrientation)
            {
                case WorldOrientation.XY:
                    {
                        forward = Vector3.right;
                    }
                    break;
                case WorldOrientation.XZ:
                case WorldOrientation.XYZ:
                    {
                        forward = Vector3.forward;
                    }
                    break;
                default:
                    throw new System.NotSupportedException();
            }

            if (Agent.WorldOrientation == WorldOrientation.XY)
            {
                var hit = Physics2D.Raycast(Agent.Position, Agent.Rotation * forward, _perceptionRange);
                if(hit.collider != null)
                {
                    return hit.normal;
                }
            }
            else
            {
                if(Physics.Raycast(Agent.Position, Agent.Rotation * forward, out var hit, _perceptionAngle))
                {
                    return hit.normal;
                }
            }

            return Vector3.zero;
        }

        protected override void DrawGizmos()
        {
            Vector3 forward;
            switch (Agent.WorldOrientation)
            {
                case WorldOrientation.XY:
                    {
                        forward = Vector3.right;
                    }
                    break;
                case WorldOrientation.XZ:
                case WorldOrientation.XYZ:
                    {
                        forward = Vector3.forward;
                    }
                    break;
                default:
                    throw new System.NotSupportedException();
            }

            Gizmos.color = Color.yellow;
            var start = Agent.Position;
            var end = Agent.Rotation * (Agent.Position + forward * _perceptionRange);
            Gizmos.DrawLine(start, end);

            var halfAngle = _perceptionAngle / 2f;

        }
    }
}