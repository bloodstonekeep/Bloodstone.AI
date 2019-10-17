using System.Collections.Generic;
using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public abstract class LocalAwarenessMovement : MovementSteering
    {
        [SerializeField]
        protected List<Agent> neighborhood;

        public List<Agent> Neighborhood
        {
            get => neighborhood;
            set => neighborhood = value;
        }
    }
}