using Bloodstone.AI.Utility;
using UnityEngine;

namespace Bloodstone.AI
{
    public class Agent : MonoBehaviour
    {
        [Header("Statistics")]
        [SerializeField]
        private WorldOrientation _worldOrientation;
        [SerializeField]
        private AgentStatistics _statistics;
        [SerializeField]
        private float _predictionRange = 1f;

        [ReadOnly]
        [SerializeField]
        private SteeringPrediction _prediction;

        public float PredictionRange => _predictionRange;
        public AgentStatistics Statistics => _statistics;
        public WorldOrientation WorldOrientation => _worldOrientation;

        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        public Vector3 Velocity { get; set; }
        public Vector3 AngularVelocity { get; set; }

        public SteeringPrediction Prediction
        {
            get => _prediction;
            set => _prediction = value;
        }
    }
}