using System.Collections.Generic;
using UnityEngine;

namespace Bloodstone.AI.Steering
{
    [RequireComponent(typeof(AISubsystem))]

    public abstract class LocalAwarenessMovement : MovementSteering
    {
        private AISubsystem _subsystem;

        protected override void Awake()
        {
            base.Awake();

            _subsystem = GetComponent<AISubsystem>();
        }

        public List<Agent> Neighborhood
        {
            get => _subsystem.Neighborhood;
        }
    }
}