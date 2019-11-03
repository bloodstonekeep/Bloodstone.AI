using System;
using UnityEngine;

namespace Bloodstone.AI.Steering
{
    [Serializable]
    public struct SteeringPrediction
    {
        public Vector3 velocity;
        public Vector3 angularVelocity;
    }
}