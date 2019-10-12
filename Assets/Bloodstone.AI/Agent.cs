using UnityEngine;

namespace Bloodstone.AI
{
    public class Agent : MonoBehaviour
    {
        [SerializeField]
        private AgentStatistics _statistics;
        [SerializeField]
        private MovementPrediction _prediction;

        public AgentStatistics Statistics => _statistics;

        public MovementPrediction Prediction
        {
            get => _prediction;
            internal set => _prediction = value;
        }
    }
}