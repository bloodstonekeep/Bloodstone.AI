using Bloodstone.AI.Steering;
using Bloodstone.AI.Steering.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bloodstone.AI.Examples.Boids
{
    public class BoidSteeringPipeline : SteeringPipeline
    {
        private readonly Dictionary<ISteeringBehaviour, Func<float>> _weightsDictionary = new Dictionary<ISteeringBehaviour, Func<float>>();
        private List<ISteeringBehaviour> _steeringBehaviours;

        [SerializeField]
        private BoidSteeringWeights _weights;

        [SerializeField]
        private Cohesion _cohesionSteering;
        [SerializeField]
        private Separation _separationSteering;
        [SerializeField]
        private VelocityMatch _velocityMatchSteering;
        [SerializeField]
        private CollisionAvoidance _collisionAvoidanceSteering;

        protected override void Awake()
        {
            base.Awake();

            RegisterSteerings();
        }

        private void RegisterSteerings()
        {
            _weightsDictionary[_cohesionSteering] = () => _weights.Cohesion;
            _weightsDictionary[_separationSteering] = () => _weights.Separation;
            _weightsDictionary[_velocityMatchSteering] = () => _weights.VelocityMatch;
            _weightsDictionary[_collisionAvoidanceSteering] = () => _weights.CollisionAvoidance;

            _steeringBehaviours = _weightsDictionary.Keys.ToList();
        }

        public override Vector3 GetMovementSteering()
        {
            var boidResult = Vector3.zero;

            foreach(var steering in _steeringBehaviours)
            {
                var weight = _weightsDictionary[steering]?.Invoke();
                if(weight.HasValue)
                {
                    boidResult += steering.GetSteering() * weight.Value;
                }
            }

            boidResult /= _steeringBehaviours.Count;

            return (base.GetMovementSteering() + boidResult) / 2;
        }
    }
}