using System.Linq;
using UnityEngine;

namespace Bloodstone.AI
{
    public class PredatorBehaviour : FlockingBehaviour
    {
        public override float GetAngularVelocity()
        {
            if (agent.Velocity == Vector2.zero)
            {
                return 0;
            }

            var currentRotation = agent.Rotation.eulerAngles.z;
            var targetRotation = Mathf.Atan2(agent.Velocity.y, agent.Velocity.x) * Mathf.Rad2Deg;

            var rotationDiff = targetRotation - currentRotation;

            while (rotationDiff > 180)
            {
                rotationDiff -= 360;
            }

            while (rotationDiff < -180)
            {
                rotationDiff += 360;
            }

            if (Mathf.Abs(rotationDiff) < 5)
            {
                rotationDiff /= 5;
            }

            return rotationDiff;
        }

        public override Vector2 GetVelocity()
        {
            var nearbyAgents = BoidsSpawner.Agents.Where(a => a.gameObject != gameObject)
                                 .Where(a => (a.Position - agent.Position).sqrMagnitude < _perceptionRadius * _perceptionRadius)
                                 .ToList();

            var nearbyPredators = BoidsSpawner.Predators.Where(a => a.gameObject != gameObject)
                                                        .Where(a => (a.Position - agent.Position).sqrMagnitude < _perceptionRadius * _perceptionRadius)
                                                        .ToList();

            var v1 = Cohesion(nearbyAgents).normalized * BoidsSpawner.CohesionWeight;
            var v2 = Separation(nearbyPredators).normalized * BoidsSpawner.SeparationWeight;

            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var v4 = Flee(mousePos).normalized;
            var v5 = CollisionAvoidance(nearbyAgents).normalized * 2f;
            var v6 = ObstacleAvoidance().normalized * 2f;

            return (v1 + v2 + v4 * 2f + v5 + v6) / 5;
        }
    }
}