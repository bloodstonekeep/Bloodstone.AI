using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Bloodstone.AI
{
    public class BoidsSpawner : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _count;

        [SerializeField]
        private float _velocityMatchWeight;
        [SerializeField]
        private float _separationWeight;
        [SerializeField]
        private float _cohesionWeight;

        private static BoidsSpawner _instance;
        public static float CohesionWeight => _instance._cohesionWeight;
        public static float VelocityMatchWeight => _instance._velocityMatchWeight;
        public static float SeparationWeight => _instance._separationWeight;

        public static List<Agent2D> Agents = new List<Agent2D>();
        public static List<Agent2D> Predators = new List<Agent2D>();

        public void SetCohesion(Slider value) => _cohesionWeight = value.value;
        public void SetVelocityMatch(Slider value) => _velocityMatchWeight = value.value;
        public void SetSeparation(Slider value) => _separationWeight = value.value;

        private void Awake()
        {
            _instance = this;
        }

        [SerializeField]
        private Agent2D _boidPrototype;
        [SerializeField]
        private Agent2D _predatorPrototype;

        [ContextMenu("Add Boid")]
        public void AddBoid()
        {
            var newAgent = Instantiate(_boidPrototype, new Vector3(Random.value * 10, Random.value * 5, 0), Quaternion.identity);
            newAgent.Velocity = RandomVector2();
            Agents.Add(newAgent);
            UpdateCounter();
        }

        [ContextMenu("Add Predator")]
        public void AddPredator()
        {
            var newAgent = Instantiate(_predatorPrototype, new Vector3(Random.value * 10, Random.value * 5, 0), Quaternion.identity);
            newAgent.Velocity = RandomVector2();
            Predators.Add(newAgent);
        }

        [ContextMenu("Add 10 Boids")]
        public void AddBoid_10()
        {
            for (int i = 0; i < 10; ++i)
            {
                AddBoid();
            }
        }

        [ContextMenu("Remove 10 Boids")]
        public void RemoveBoid_10()
        {
            if (Agents.Count > 0)
            {
                var startCount = Agents.Count;
                for (int i = Agents.Count - 1; i > startCount - 1 - 10; --i)
                {
                    Destroy(Agents[i].gameObject);
                    Agents.RemoveAt(i);
                }

                UpdateCounter();
            }
        }

        public Vector2 RandomVector2()
        {
            return new Vector2(Random.value, Random.value);
        }

        private void UpdateCounter()
        {
            _count.text = Agents.Count.ToString();
        }
    }
}