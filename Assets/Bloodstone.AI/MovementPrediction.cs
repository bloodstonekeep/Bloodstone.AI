
using System;
using UnityEngine;

namespace Bloodstone.AI
{
    [Serializable]
    public struct MovementPrediction
    {
        public Vector3 Velocity;
        public Vector3 AngularVelocity;
    }
}