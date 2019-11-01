using System.Collections.Generic;
using Bloodstone.AI.Steering;
using UnityEngine;

namespace Bloodstone.AI
{
    [ExecuteAlways]
    sealed public class AISubsystem : MonoBehaviour
    {
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