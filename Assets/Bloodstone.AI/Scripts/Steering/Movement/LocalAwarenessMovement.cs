using System.Collections.Generic;
using UnityEngine;

namespace Bloodstone.AI.Steering.Movement
{
    [RequireComponent(typeof(AISubsystem))]

    public abstract class LocalAwarenessMovement : MovementSteering
    {
        public List<Agent> Neighbourhood
        {
            get => subsystem.Neighbourhood;
        }
    }
}