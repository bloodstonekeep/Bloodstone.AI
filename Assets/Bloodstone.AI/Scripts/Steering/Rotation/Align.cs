using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public class Align : RotationSteering
    {
        private const float gizmosLength = 3f;

        [SerializeField]
        private bool _showGizmos;

        public Quaternion TargetRotation;

        public override Vector3 GetSteering()
        {
            var rotation = Quaternion.Inverse(Agent.Rotation) * TargetRotation;

            var angle = 2 * Mathf.Acos(rotation.w);
            var halfSin = Mathf.Sin(angle / 2);
            if (halfSin == 0)
            {
                return Vector3.zero;
            }

            angle *= Mathf.Rad2Deg;
            var axis = new Vector3
            {
                x = rotation.x / halfSin,
                y = rotation.y / halfSin,
                z = rotation.z / halfSin
            };

            while (angle > 180)
            {
                angle -= 360;
            }

            while (angle < -180)
            {
                angle += 360;
            }

            var result = axis * angle * Agent.Statistics.MaximumAngularSpeed;

            return result;
        }

        private void OnDrawGizmos()
        {
            if (_showGizmos)
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

                var targetRotation = TargetRotation * forward;

                forward = Agent.Rotation * forward;

                Gizmos.color = Color.red;
                Gizmos.DrawLine(Agent.Position + forward, Agent.Position + forward * gizmosLength);
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(Agent.Position + targetRotation, Agent.Position + targetRotation * gizmosLength);
            }
        }
    }
}