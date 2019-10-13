using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bloodstone.AI.Steering
{
    [RequireComponent(typeof(AISubsystem))]
    public class SteeringPipeline : MonoBehaviour, ISteeringPipeline
    {
        private AISubsystem _subsystem;
        private Agent _agent;

        [SerializeField]
        private List<WeightedMovement> _movement;
        [SerializeField]
        private List<WeightedRotation> _rotation;

        private void Awake()
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

        public Vector3 MovementSteering()
        {
            return BlendSteerings<WeightedMovement, MovementSteering>(_movement);
        }

        public Vector3 RotationSteering()
        {
            return BlendSteerings<WeightedRotation, RotationSteering>(_rotation);
        }

        private Vector3 BlendSteerings<T, T2>(IList<T> input) where T : WeightedSteering<T2> where T2 : ISteeringBehaviour
        {
            if(input.Count == 0)
            {
                return Vector3.zero;
            }

            Vector3 result = new Vector3();
            for(int i = 0; i < input.Count; ++i)
            {
                var w = input[i].Weight;
                if (w == 0)
                {
                    continue;
                }
                result += input[i].Behaviour.GetSteering();
            }

            return result / input.Count;
        }
    }

    public class WeightedSteering<T>
    {
        [SerializeField]
        private T _behaviour;

        [SerializeField]
        private float _weight = 1f;

        public float Weight
        {
            get => _weight;
            set => _weight = value;
        }

        public T Behaviour
        {
            get => _behaviour;
            set => _behaviour = value;
        }
    }

    [Serializable]
    public class WeightedMovement : WeightedSteering<MovementSteering>
    {
    }

    [Serializable]
    public class WeightedRotation : WeightedSteering<RotationSteering>
    {
    }
}