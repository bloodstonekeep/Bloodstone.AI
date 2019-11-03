using UnityEngine;

namespace Bloodstone.AI.Steering.Movement
{
    public class Separation : LocalAwarenessMovement
    {
        [SerializeField]
        private float _threshold = 1f;

        [SerializeField]
        private float _decayCoefficient = -0.1f;

        public float SquaredThreshold => _threshold * _threshold;

        public override Vector3 GetSteering()
        {
            if (Neighbourhood.Count == 0)
            {
                return Vector3.zero;
            }

            return CalculateSeparationForce();
        }

        protected override void DrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Agent.Position, _threshold);
        }

        private Vector3 CalculateSeparationForce()
        {
            Vector3 netForce = Vector3.zero;

            foreach (var agent in Neighbourhood)
            {
                var translation = agent.Position - Agent.Position;
                var distance = translation.sqrMagnitude;

                if (distance == 0)
                {
                    continue;
                }

                if (distance < SquaredThreshold)
                {
                    var strength = Mathf.Min(_decayCoefficient / distance, Agent.Statistics.speed);

                    netForce += translation.normalized * strength;
                }
            }

            return netForce / Neighbourhood.Count;
        }
    }
}