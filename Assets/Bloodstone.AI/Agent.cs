using UnityEngine;

namespace Bloodstone.AI
{
    public class Agent : MonoBehaviour
    {
        [SerializeField]
        private AgentStatistics _statistics;

        [SerializeField]
        private Vector3 _velocity;

        [SerializeField]
        private Vector3 _angularVelocity;

        public Vector3 Velocity
        {
            get => _velocity;
            set => _velocity = value;
        }

        public Vector3 AngularVelocity
        {
            get => _angularVelocity;
            set => _angularVelocity = value;
        }

        public AgentStatistics Statistics => _statistics;
    }
}