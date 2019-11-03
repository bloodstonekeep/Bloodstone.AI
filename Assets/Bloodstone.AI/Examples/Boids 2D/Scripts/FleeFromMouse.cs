using Bloodstone.AI.Steering.Movement;
using UnityEngine;

namespace Bloodstone.AI.Examples
{
    public class FleeFromMouse : Seek
    {
        private Camera _camera;

        protected override void Awake()
        {
            base.Awake();

            _camera = Camera.main;
        }

        public override Vector3 GetSteering()
        {
            TargetPosition = GetMousePosition();

            var distance = TargetPosition - Agent.Position;
            if (distance.sqrMagnitude < Agent.PredictionRange * Agent.PredictionRange)
            {
                return -base.GetSteering();
            }

            return Vector3.zero;
        }

        private Vector3 GetMousePosition()
        {
            var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z -= _camera.transform.position.z;
            return mousePos;
        }

    }
}