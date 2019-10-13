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

            rotation.ToAngleAxis(out float angle, out Vector3 axis);

            while (angle > 180)
            {
                angle -= 360;
            }

            while (angle < -180)
            {
                angle += 360;
            }

            return axis * angle * Agent.Statistics.MaximumAngularSpeed;
        }

        private void OnDrawGizmos()
        {
            if(_showGizmos)
            {
                Vector3 forward;

                switch(Agent.Plane)
                {
                    case Agent.PlaneMode.XY:
                        {
                            forward = Vector3.right;
                        }
                        break;
                    case Agent.PlaneMode.XZ:
                    case Agent.PlaneMode.XYZ:
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