using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public class Align : RotationSteering
    {
        private const float Tau = 6.2831852f;
        private const float HalfTau = Mathf.PI;
        private const float GizmosLength = 3f;

        [SerializeField]
        private bool _showGizmos;

        public Quaternion TargetRotation;

        public override Vector3 GetSteering()
        {
            var rotation = (Quaternion.Inverse(Agent.Rotation) * TargetRotation).normalized;

            var angle = 2 * Mathf.Acos(rotation.w);
            var halfSin = Mathf.Sin(angle / 2);
            if (halfSin == 0)
            {
                return Vector3.zero;
            }

            var axis = new Vector3
            {
                x = rotation.x / halfSin,
                y = rotation.y / halfSin,
                z = rotation.z / halfSin
            };

            angle = NormalizeAngle(angle);

            return axis * angle * Mathf.Rad2Deg * Agent.Statistics.MaximumAngularSpeed;
        }

        private static float NormalizeAngle(float angle)
        {
            angle %= Tau;

            if (angle > HalfTau)
            {
                angle -= Tau;
            }

            if (angle < -HalfTau)
            {
                angle += Tau;
            }

            return angle;
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
                Gizmos.DrawLine(Agent.Position + forward, Agent.Position + forward * GizmosLength);
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(Agent.Position + targetRotation, Agent.Position + targetRotation * GizmosLength);
            }
        }
    }
}