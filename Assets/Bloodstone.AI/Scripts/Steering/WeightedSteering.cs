using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public class WeightedSteering<T>
    {
        [SerializeField]
        private T _behaviour;

        [SerializeField]
        private float _weight = 1f;

        public float Weight
        {
            get => _weight;
            set => _weight = value;
        }

        public T Behaviour
        {
            get => _behaviour;
            set => _behaviour = value;
        }
    }
}