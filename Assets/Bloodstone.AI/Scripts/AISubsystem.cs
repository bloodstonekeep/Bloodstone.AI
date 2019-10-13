using System.Collections.Generic;
using Bloodstone.AI.Steering;
using UnityEngine;

namespace Bloodstone.AI
{
    [RequireComponent(typeof(Agent))]
    sealed public class AISubsystem : MonoBehaviour
    {
        private List<ISteeringPipeline> _agentsPipelines = new List<ISteeringPipeline>();
        private Agent _agent;

        private void Awake()
        {
            _agent = GetComponent<Agent>();
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

            _agent.Prediction = newSteering;
        }
    }
}