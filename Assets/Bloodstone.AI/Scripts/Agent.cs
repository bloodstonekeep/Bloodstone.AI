using UnityEngine;

namespace Bloodstone.AI
{
    public class Agent : MonoBehaviour
    {
        [SerializeField]
        private WorldOrientation _worldOrientation;

        [SerializeField]
        private Vector3 _velocity;
        [SerializeField]
        private Vector3 _angularVelocity;

        [SerializeField]
        private AgentStatistics _statistics;

        [SerializeField]
        private SteeringPrediction _prediction;

        public Vector3 Position
        {
            get;
            set;
        }
        public Quaternion Rotation
        {
            get;
            set;
        }

        public WorldOrientation WorldOrientation => _worldOrientation;
        public AgentStatistics Statistics => _statistics;

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

        public SteeringPrediction Prediction
        {
            get => _prediction;
            set => _prediction = value;
        }
    }
}