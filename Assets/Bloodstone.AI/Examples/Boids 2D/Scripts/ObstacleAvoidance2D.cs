using System;
using UnityEngine;
using Bloodstone.AI.Utility;
using Bloodstone.AI.Steering.Movement;

namespace Bloodstone.AI.Examples.Boids
{
    public class ObstacleAvoidance2D : LocalAwarenessMovement
    {
        private const int SensorsCount = 3;

        [LayerIndex]
        [SerializeField]
        private int _obstacleLayerIndex = 1 << 8;

        [Range(0, Mathf.PI)]
        [SerializeField]
        [Tooltip("Angle (in radians) between sensors")]
        private float _subsensorsAngle = Mathf.PI / 3f;

        [SerializeField]
        protected float avoidanceDistance = .5f;

        private readonly SensorsData _sensorsData = new SensorsData();

        public override Vector3 GetSteering()
        {
            Vector3 result = Vector3.zero;

            Vector2 forward = Agent.Velocity;
            result += SensorRaycast(forward);

            var leftDir = forward.Rotate(_sensorsData.LeftSubsensorSin, _sensorsData.LeftSubsensorCos);
            result += SensorRaycast(leftDir);

            var rightDir = forward.Rotate(_sensorsData.RightSubsensorSin, _sensorsData.RightSubsensorCos);
            result += SensorRaycast(rightDir);

            return result;
        }

        protected override void Awake()
        {
            base.Awake();

            _sensorsData.UpdateAngle(_subsensorsAngle);
        }

        private Vector3 SensorRaycast(Vector3 direction)
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
            var hit = Physics2D.Raycast(Agent.Position, direction.normalized, Agent.PredictionRange, 1 << _obstacleLayerIndex);
            var collider = hit.collider;

            if (collider != null
                && !collider.isTrigger)
            {
                var targetPosition = hit.point + hit.normal * avoidanceDistance;
                return targetPosition.ToVector3() - Agent.Position;
            }

            return Vector3.zero;
        }

        private void OnValidate()
        {
            _sensorsData.UpdateAngle(_subsensorsAngle);
        }

        private class SensorsData
        {
            public void UpdateAngle(float angle)
            {
                var cosinusOfAngle = Mathf.Cos(angle);
                LeftSubsensorCos = cosinusOfAngle;
                RightSubsensorCos = cosinusOfAngle;

                LeftSubsensorSin = Mathf.Sin(angle);
                RightSubsensorSin = Mathf.Sin(-angle);
            }

            public float LeftSubsensorCos { get; private set; }
            public float LeftSubsensorSin { get; private set; }
            public float RightSubsensorCos { get; private set; }
            public float RightSubsensorSin { get; private set; }
        }
    }
}