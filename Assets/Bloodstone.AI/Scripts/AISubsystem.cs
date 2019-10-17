using System.Collections.Generic;
using System.Linq;
using Bloodstone.AI.Steering;
using UnityEngine;

namespace Bloodstone.AI
{
    [RequireComponent(typeof(Agent))]
    sealed public class AISubsystem : MonoBehaviour
    {
        private List<ISteeringPipeline> _agentsPipelines = new List<ISteeringPipeline>();
        private Agent _agent;

        public List<Agent> Neighborhood { get; set; }

        private void Awake()
        {
            _agent = GetComponent<Agent>();
            Neighborhood = FindObjectsOfType<Agent>().Where(a => a != _agent)
                                                        .ToList();
        }

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