using System.Collections.Generic;
using UnityEngine;

namespace Bloodstone.AI.Steering
{
    [RequireComponent(typeof(AISubsystem))]

    public abstract class LocalAwarenessMovement : MovementSteering
    {
        public List<Agent> Neighborhood
        {
            get => subsystem.Neighborhood;
        }
    }
}