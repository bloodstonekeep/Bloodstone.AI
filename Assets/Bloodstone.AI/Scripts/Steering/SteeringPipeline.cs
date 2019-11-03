using System;
using System.Collections.Generic;
using Bloodstone.AI.Steering.Movement;
using Bloodstone.AI.Steering.Rotation;
using UnityEngine;

namespace Bloodstone.AI.Steering
{
    [RequireComponent(typeof(AISubsystem))]
    public class SteeringPipeline : MonoBehaviour, ISteeringPipeline
    {
        private AISubsystem _subsystem;

        [SerializeField]
        protected List<WeightedMovement> movementBehaviours;
        [SerializeField]
        protected List<WeightedRotation> rotationBehaviours;

        protected virtual void Awake()
        {
            _subsystem = GetComponent<AISubsystem>();
        }

        private void OnEnable()
        {
            _subsystem.Add(this);
        }

        private void OnDisable()
        {
            _subsystem.Remove(this);
        }

        public virtual Vector3 GetMovementSteering()
        {
            return BlendSteerings<WeightedMovement, MovementSteering>(movementBehaviours);
        }

        public virtual Vector3 GetRotationSteering()
        {
            return BlendSteerings<WeightedRotation, RotationSteering>(rotationBehaviours);
        }

        private Vector3 BlendSteerings<T, T2>(IList<T> input) where T : WeightedSteering<T2> where T2 : ISteeringBehaviour
        {
            if (input.Count == 0)
            {
                return Vector3.zero;
            }

            Vector3 result = new Vector3();
            for (int i = 0; i < input.Count; ++i)
            {
                var w = input[i].Weight;
                if (w == 0)
                {
                    continue;
                }
                result += input[i].Behaviour.GetSteering() * w;
            }

            return result / input.Count;
        }

        [Serializable]
        protected class WeightedMovement : WeightedSteering<MovementSteering>
        {
        }

        [Serializable]
        protected class WeightedRotation : WeightedSteering<RotationSteering>
        {
        }
    }
}