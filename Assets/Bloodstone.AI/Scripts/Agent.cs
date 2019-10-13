using UnityEngine;

namespace Bloodstone.AI
{
    public class Agent : MonoBehaviour
    {
        [SerializeField]
        private PlaneMode _plane;

        [SerializeField]
        private Vector3 _velocity;
        [SerializeField]
        private Vector3 _angularVelocity;

        [SerializeField]
        private AgentStatistics _statistics;

        [SerializeField]
        private SteeringPrediction _prediction;

        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        public PlaneMode Plane => _plane;
        public AgentStatistics Statistics => _statistics;

        public SteeringPrediction Prediction
        {
            get => _prediction;
            internal set => _prediction = value;
        }

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

        public enum PlaneMode
        {
            XY,
            XZ,
            XYZ,
        }
    }
}