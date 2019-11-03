using UnityEngine;

namespace Bloodstone.AI.Examples.Boids
{
    [CreateAssetMenu]
    public class BoidSteeringWeights : ScriptableObject
    {
        public float cohesion;
        public float separation;
        public float velocityMatch;
        public float collisionAvoidance;
    }
}