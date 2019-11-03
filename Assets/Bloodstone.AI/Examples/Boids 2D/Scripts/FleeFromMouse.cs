using Bloodstone.AI.Steering.Movement;
using UnityEngine;

namespace Bloodstone.AI.Examples
{
    public class FleeFromMouse : Seek
    {
        private float _separationRadius = 1;

        public override Vector3 GetSteering()
        {
            var camera = Camera.main;

            var mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z -= camera.transform.position.z;
            TargetPosition = mousePos;

            var dist = mousePos - Agent.Position;
            if (dist.sqrMagnitude < _separationRadius * _separationRadius)
            {
                return -base.GetSteering();
            }

            return Vector3.zero;
        }
    }
}