using UnityEngine;
using UnityEngine.Assertions;

namespace Bloodstone.AI.Examples.Boids
{
    public class BoidsFactory : MonoBehaviour
    {
        [SerializeField]
        private Transform _boidsParent;

        [SerializeField]
        private GameObject _boidPrototype;

        [ContextMenu("Add Boid")]
        public void AddBoid()
        {
            var newBoid = Instantiate(_boidPrototype, new Vector3(Random.value * 10, Random.value * 5, 0), Quaternion.identity, _boidsParent);
            var agent = newBoid.GetComponentInChildren<Agent>();
            agent.Prediction = new SteeringPrediction()
            {
                Velocity = RandomVector2()
            };
        }

        [ContextMenu("Add Boid 10")]
        public void AddBoid_10()
        {
            for (int i = 0; i < 10; ++i)
            {
                AddBoid();
            }
        }

        public Vector2 RandomVector2()
        {
            return new Vector2(Random.value, Random.value);
        }

        private void OnValidate()
        {
            if (_boidPrototype != null
                && !_boidPrototype.GetComponentInChildren<Agent>())
            {
                _boidPrototype = null;
            }

            Assert.IsNotNull(_boidPrototype);
        }
    }
}