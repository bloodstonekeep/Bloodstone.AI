using System;
using UnityEngine;
using Bloodstone.AI.Steering;

namespace Bloodstone.AI.Examples.Boids
{
    public class ObstacleAvoidance2D : LocalAwarenessMovement
    {
        public float PerceptionRadius = 1f;

        [SerializeField]
        protected float _avoidance = .5f;

        [SerializeField]
        private float _sensorsAngle = Mathf.PI / 3f;

        // todo: read only
        // todo: struct
        [SerializeField]
        private float _leftSensorCos;
        [SerializeField]
        private float _leftSensorSin;
        [SerializeField]
        private float _rightSensorCos;
        [SerializeField]
        private float _rightSensorSin;

        public override Vector3 GetSteering()
        {
            Vector3 result = Vector3.zero;

            var forward = Agent.Velocity;
            var leftDir = new Vector2(_leftSensorCos * Agent.Velocity.x - _leftSensorSin * Agent.Velocity.y, _leftSensorSin * Agent.Velocity.x + _leftSensorCos * Agent.Velocity.y);
            var rightDir = new Vector2(_rightSensorCos * Agent.Velocity.x - _rightSensorSin * Agent.Velocity.y, _rightSensorSin * Agent.Velocity.x + _rightSensorCos * Agent.Velocity.y);

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

        private void OnValidate()
        {
            _leftSensorCos = Mathf.Cos(_sensorsAngle);
            _leftSensorSin = Mathf.Sin(_sensorsAngle);

            _rightSensorCos = Mathf.Cos(-_sensorsAngle);
            _rightSensorSin = Mathf.Sin(-_sensorsAngle);
        }
    }
}