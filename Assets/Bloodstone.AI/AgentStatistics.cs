using UnityEngine;

namespace Bloodstone.AI
{
    public struct AgentStatistics
    {
        [SerializeField]
        private float _maximumSpeed;

        [SerializeField]
        private float _maximumAngularSpeed;

        public float MaximumSpeed
        {
            get => _maximumSpeed;
            set => _maximumSpeed = value;
        }

        public float MaximumAngularSpeed
        {
            get => _maximumAngularSpeed;
            set => _maximumAngularSpeed = value;
        }
    }
}