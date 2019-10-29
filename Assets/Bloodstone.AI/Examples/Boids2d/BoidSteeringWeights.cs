using UnityEngine;

namespace Bloodstone.AI.Examples.Boids
{
    [CreateAssetMenu]
    public class BoidSteeringWeights : ScriptableObject
    {
        public float Cohesion;
        public float Separation;
        public float VelocityMatch;
        public float CollisionAvoidance;
    }
}