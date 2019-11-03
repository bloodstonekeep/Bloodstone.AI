using System.Collections.Generic;
using Bloodstone.AI.Steering;
using UnityEngine;

namespace Bloodstone.AI
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Agent))]
    public sealed class AISubsystem : MonoBehaviour
    {
        private Agent _agent;

        private readonly List<ISteeringPipeline> _agentPipelines = new List<ISteeringPipeline>();

        public List<Agent> Neighbourhood { get; set; } = new List<Agent>();

        public void Add(ISteeringPipeline steeringPipeline)
        {
            _agentPipelines.Add(steeringPipeline);
        }

        public void Remove(ISteeringPipeline steeringPipeline)
        {
            _agentPipelines.Remove(steeringPipeline);
        }

        private void Awake()
        {
            _agent = GetComponent<Agent>();
        }

        private void Update()
        {
            _agent.Prediction = CalulateNewPrediction();
        }

        private SteeringPrediction CalulateNewPrediction()
        {
            var newSteering = new SteeringPrediction();

            var pipelinesCount = _agentPipelines.Count;
            if (pipelinesCount != 0)
            {
                foreach (var pipeline in _agentPipelines)
                {
                    newSteering.velocity += pipeline.GetMovementSteering();
                    newSteering.angularVelocity += pipeline.GetRotationSteering();
                }

                newSteering.velocity /= pipelinesCount;
                newSteering.angularVelocity /= pipelinesCount;
            }

            return newSteering;
        }
    }
}