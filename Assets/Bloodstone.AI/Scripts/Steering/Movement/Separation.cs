using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public class Separation : LocalAwarenessMovement
    {
        [SerializeField]
        private float _threshold = 1f;

        [SerializeField]
        private float _decayCoefficient = 1f;

        public float SquaredThreshold => _threshold * _threshold;

        public override Vector3 GetSteering()
        {
            if(Neighborhood.Count == 0)
            {
                return Vector3.zero;
            }

            Vector3 netForce = Vector3.zero;

            foreach (var agent in Neighborhood)
            {
                var translation = (agent.Position - Agent.Position);
                var distance = translation.sqrMagnitude;
                if(distance == 0)
                {
                    continue;
                }

                if(distance < SquaredThreshold)
                {
                    var strength = Mathf.Min(_decayCoefficient / distance, Agent.Statistics.MaximumSpeed);
                    netForce += translation.normalized * strength;
                }
            }

            //return netForce.normalized * Agent.Statistics.MaximumSpeed;
            return netForce;
        }

        protected override void DrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Agent.Position, _threshold);
        }
    }
}