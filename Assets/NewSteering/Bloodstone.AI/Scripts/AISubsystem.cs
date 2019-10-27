using System.Collections.Generic;
using System.Linq;
using Bloodstone.AI.Steering;
using UnityEngine;

namespace Bloodstone.AI
{
    [ExecuteAlways]
    sealed public class AISubsystem : MonoBehaviour
    {
        [SerializeField]
        private float _predictionRange = 1f;
        [SerializeField]
        private Agent _agent;

        private List<ISteeringPipeline> _agentsPipelines = new List<ISteeringPipeline>();

        public List<Agent> Neighborhood { get; set; }
        public Agent Agent => _agent;

        public void Add(ISteeringPipeline steeringPipeline)
        {
            _agentsPipelines.Add(steeringPipeline);
        }

        public void Remove(ISteeringPipeline steeringPipeline)
        {
            _agentsPipelines.Remove(steeringPipeline);
        }

        public void Update()
        {
            Neighborhood = FindObjectsOfType<Agent>().Where(a => a != _agent)
                                            .Where(a => (a.transform.position - _agent.transform.position).sqrMagnitude < _predictionRange * _predictionRange)
                                            .ToList();

            var newSteering = new SteeringPrediction();

            foreach (var pipeline in _agentsPipelines)
            {
                newSteering.Velocity += pipeline.MovementSteering();
                newSteering.AngularVelocity += pipeline.RotationSteering();
            }

            int count = _agentsPipelines.Count;
            if (count != 0)
            {
                newSteering.Velocity /= count;
                newSteering.AngularVelocity /= count;
            }

            _agent.Prediction = newSteering;
        }
    }
}