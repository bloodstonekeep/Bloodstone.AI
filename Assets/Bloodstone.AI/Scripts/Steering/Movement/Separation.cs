using System.Collections.Generic;
using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public class Separation : MovementSteering
    {
        [SerializeField]
        private List<Agent> _targets;

        [SerializeField]
        private float _threshold = 1f;

        [SerializeField]
        private float _decayCoefficient = 1f;

        public float SquaredThreshold => _threshold * _threshold;

        public override Vector3 GetSteering()
        {
            Vector3 netForce = Vector3.zero;

            foreach(var target in _targets)
            {
                var translation = (target.Position - Agent.Position);
                var distance = translation.sqrMagnitude;

                Debug.Log(distance);
                if(distance < SquaredThreshold)
                {
                    var strength = Mathf.Min(_decayCoefficient / distance, Agent.Statistics.MaximumSpeed);
                    netForce += translation.normalized * strength;
                }
            }

            return netForce;
        }

        protected override void DrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Agent.Position, _threshold);
        }
    }
}