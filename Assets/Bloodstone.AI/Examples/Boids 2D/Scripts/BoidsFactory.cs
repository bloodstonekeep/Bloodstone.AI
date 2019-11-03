using Bloodstone.AI.Steering;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bloodstone.AI.Examples.Boids
{
    public class BoidsFactory : MonoBehaviour
    {
        [SerializeField]
        private Rect _spawnRect;

        [SerializeField]
        private Transform _boidsParent;

        [SerializeField]
        private Boid _boidPrototype;

        public (Boid boid, AISubsystem subsystem) CreateNewBoid()
        {
            var newBoid = Instantiate(_boidPrototype, GetNewBoidPosition(), Quaternion.identity, _boidsParent);
            var agent = newBoid.GetComponentInChildren<Agent>();
            agent.Prediction = new SteeringPrediction()
            {
                velocity = RandomVector2()
            };

            var subsystem = newBoid.GetComponentInChildren<AISubsystem>();
            return (newBoid, subsystem);
        }

        private Vector3 GetNewBoidPosition()
        {
            return new Vector3(
                x: _spawnRect.x + Random.value * _spawnRect.width,
                y: _spawnRect.y + Random.value * _spawnRect.height);
        }

        private Vector2 RandomVector2()
        {
            return new Vector2(Random.value, Random.value);
        }

        private void OnValidate()
        {
            if (_boidPrototype != null
                && !_boidPrototype.GetComponentInChildren<Agent>()
                && !_boidPrototype.GetComponentInChildren<AISubsystem>())
            {
                _boidPrototype = null;
            }

            Assert.IsNotNull(_boidPrototype);
        }
    }
}