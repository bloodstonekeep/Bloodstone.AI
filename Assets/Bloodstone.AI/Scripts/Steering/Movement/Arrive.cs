using UnityEngine;

namespace Bloodstone.AI.Steering.Movement
{
    public class Arrive : Seek
    {
        [SerializeField]
        private float _arriveRadius = 1f;

        protected float SquaredArriveRadius => _arriveRadius * _arriveRadius;

        public override Vector3 GetSteering()
        {
            var distance = TargetPosition - Agent.Position;
            if(distance.sqrMagnitude < SquaredArriveRadius)
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