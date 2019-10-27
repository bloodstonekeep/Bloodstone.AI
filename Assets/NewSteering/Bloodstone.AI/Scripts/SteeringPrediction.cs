
using System;
using UnityEngine;

namespace Bloodstone.AI
{
    [Serializable]
    public struct SteeringPrediction
    {
        public Vector3 Velocity;
        public Vector3 AngularVelocity;
    }
}