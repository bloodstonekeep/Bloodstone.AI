using Bloodstone.AI.Steering;
using UnityEngine;

namespace Bloodstone.AI
{
    public class Arrive : Seek
    {
        [SerializeField]
        private float _arriveRadius = 1f;

        protected float ArriveRadiusSquared => _arriveRadius * _arriveRadius;

        public override Vector3 GetSteering()
        {
            var distance = TargetPosition - Agent.Position;
            if(distance.sqrMagnitude < ArriveRadiusSquared)
            {
                return Vector3.zero;
            }

            return base.GetSteering();
        }

        protected override void DrawGizmos()
        {
            base.DrawGizmos();

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(TargetPosition, _arriveRadius);
        }
    }
}