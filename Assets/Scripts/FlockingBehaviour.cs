using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bloodstone.AI
{

    public class FlockingBehaviour : Behaviour
    {
        [SerializeField]
        protected float _perceptionRadius;
        [SerializeField]
        protected float _separationRadius;
        [SerializeField]
        protected float _radius = 0.1f;
        [SerializeField]
        protected float _avoidance = .5f;

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
            var v2 = Separation(nearbyAgents).normalized * BoidsSpawner.SeparationWeight;
            var v3 = VelocityMatch(nearbyAgents).normalized * BoidsSpawner.VelocityMatchWeight;

            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var v4 = Flee(mousePos).normalized;
            var v5 = CollisionAvoidance(nearbyAgents).normalized * 2f;
            var v6 = ObstacleAvoidance().normalized * 2f;
            var v7 = PredatorAvoidance(nearbyPredators).normalized * 2f;

            return (v1 + v2 + v3 + v4 * 2f + v5 + v6) / 6;
        }

        public Vector2 ObstacleAvoidance()
        {
            const float angle = Mathf.PI / 3f;

            Vector2 result = Vector2.zero;

            //Debug.DrawLine(agent.Position, agent.Position + agent.Velocity.normalized * _perceptionRadius, Color.yellow);
            var hit = Physics2D.Raycast(agent.Position, agent.Velocity.normalized, _perceptionRadius);
            if (hit.collider != null)
            {
                var targetPos = hit.point + hit.normal * _avoidance;
                result += targetPos - agent.Position;
            }

            var cosl = Mathf.Cos(angle);
            var sinl = Mathf.Sin(angle);
            var leftDir = new Vector2(cosl * agent.Velocity.x - sinl * agent.Velocity.y, sinl * agent.Velocity.x + cosl * agent.Velocity.y);

            //Debug.DrawLine(agent.Position, agent.Position + leftDir.normalized * _perceptionRadius, Color.yellow);

            hit = Physics2D.Raycast(agent.Position, leftDir.normalized, _perceptionRadius);
            if (hit.collider != null)
            {
                var targetPos = hit.point + hit.normal * _avoidance;
                result += targetPos - agent.Position;
            }

            var cosr = Mathf.Cos(-angle);
            var sinr = Mathf.Sin(-angle);
            var rightDir = new Vector2(cosr * agent.Velocity.x - sinr * agent.Velocity.y, sinr * agent.Velocity.x + cosr * agent.Velocity.y);

            //Debug.DrawLine(agent.Position, agent.Position + rightDir.normalized * _perceptionRadius, Color.yellow);

            hit = Physics2D.Raycast(agent.Position, rightDir.normalized, _perceptionRadius);
            if (hit.collider != null)
            {
                var targetPos = hit.point + hit.normal * _avoidance;
                result += targetPos - agent.Position;
            }

            return result;
        }

        private Vector2 PredatorAvoidance(List<Agent2D> predators)
        {
            if (predators.Count == 0)
            {
                return Vector2.zero;
            }

            Vector2 netForce = Vector2.zero;
            foreach (var p in predators)
            {
                netForce += Flee(p.Position);
            }

            return netForce;
        }

        public Vector2 Flee(Vector2 position)
        {
            var dist = position - agent.Position;
            if (dist.sqrMagnitude < _separationRadius * _separationRadius + 1)
            {
                return -dist;
            }

            return Vector2.zero;
        }

        public Vector2 Cohesion(List<Agent2D> agents)
        {
            Vector2 averagePoint = Vector2.zero;
            if (agents.Count == 0)
            {
                return averagePoint;
            }

            foreach (var a in agents)
            {
                averagePoint += a.Position;
            }
            averagePoint /= agents.Count;

            return averagePoint - agent.Position;
        }

        public Vector2 CollisionAvoidance(List<Agent2D> agents)
        {
            if (agents.Count == 0)
            {
                return Vector2.zero;
            }

            float maxPredictionDistance = _radius * 1.5f;

            float timeToCollision = float.MaxValue;
            Agent2D target = null;

            foreach (var a in agents)
            {
                var relativePos = (agent.Position - a.Position);
                if (relativePos.sqrMagnitude > maxPredictionDistance * maxPredictionDistance)
                {
                    continue;
                }

                var relativeVelocity = (agent.Velocity - a.Velocity);
                var estimatedTime = -Vector2.Dot(relativePos, relativeVelocity) / relativeVelocity.sqrMagnitude;

                if (estimatedTime > 0 && estimatedTime < timeToCollision)
                {
                    target = a;
                    timeToCollision = estimatedTime;
                }
            }

            if (target == null)
            {
                return Vector2.zero;
            }

            Vector2 result;

            if (timeToCollision <= 0 || (agent.Position - target.Position).sqrMagnitude < _radius * _radius)
            {
                result = agent.Position - target.Position;
            }
            else
            {
                var pos = agent.Position - target.Position;
                var vel = agent.Velocity - target.Velocity;

                result = pos + vel * timeToCollision;
            }

            return result;
        }

        public Vector2 Separation(List<Agent2D> agents)
        {
            Vector2 separationVector = Vector2.zero;
            int separatedCount = 0;
            foreach (var a in agents)
            {
                var interpolation = a.Position - agent.Position;

                if (interpolation.sqrMagnitude < _separationRadius * _separationRadius)
                {
                    separationVector += interpolation * ((_separationRadius * _separationRadius) - interpolation.sqrMagnitude) / _separationRadius * _separationRadius;
                    ++separatedCount;
                }
            }

            if (separatedCount != 0)
            {
                separationVector /= separatedCount;
            }

            return -separationVector;
        }

        private Vector2 VelocityMatch(List<Agent2D> nearbyBoids)
        {
            Vector2 averageDirection = Vector2.zero;

            if (nearbyBoids.Count == 0)
            {
                return averageDirection;
            }

            foreach (var a in nearbyBoids)
            {
                averageDirection += a.Velocity;
            }

            return averageDirection / nearbyBoids.Count;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(agent.Position, _perceptionRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(agent.Position, _separationRadius);
        }
    }
}