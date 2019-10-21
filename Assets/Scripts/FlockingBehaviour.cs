using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlockingBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _perceptionRadius;
    [SerializeField]
    private float _separationRadius;
    [SerializeField]
    private float _radius = 0.1f;
    [SerializeField]
    private float _avoidance = .5f;

    private Agent2D _agent;

    private void Awake()
    {
        _agent = GetComponent<Agent2D>();
    }

    public float GetAngularVelocity()
    {
        if (_agent.Velocity == Vector2.zero)
        {
            return 0;
        }

        var currentRotation = _agent.Rotation.eulerAngles.z;
        var targetRotation = Mathf.Atan2(_agent.Velocity.y, _agent.Velocity.x) * Mathf.Rad2Deg;

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

    public Vector2 GetVelocity()
    {
        var nearbyAgents = BoidsSpawner.Agents.Where(a => a.gameObject != gameObject)
                             .Where(a => (a.Position - _agent.Position).sqrMagnitude < _perceptionRadius * _perceptionRadius)
                             .ToList();

        var v1 = Cohesion(nearbyAgents).normalized * BoidsSpawner.CohesionWeight;
        var v2 = Separation(nearbyAgents).normalized * BoidsSpawner.SeparationWeight;
        var v3 = VelocityMatch(nearbyAgents).normalized * BoidsSpawner.VelocityMatchWeight;

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var v4 = Flee(mousePos).normalized;
        var v5 = CollisionAvoidance(nearbyAgents).normalized;
        var v6 = ObstacleAvoidance().normalized;

        return (v1 + v2 + v3 + v4 * 4f + v5 + v6) / 6;
    }

    private Vector2 ObstacleAvoidance()
    {
        const float angle = Mathf.PI / 3f;

        Debug.DrawLine(_agent.Position, _agent.Position + _agent.Velocity.normalized * _perceptionRadius, Color.yellow);
        var hit = Physics2D.Raycast(_agent.Position, _agent.Velocity.normalized, _perceptionRadius);
        if (hit.collider != null)
        {
            var targetPos = hit.point + hit.normal * _avoidance;
            return targetPos - _agent.Position;
        }

        var cosl = Mathf.Cos(angle);
        var sinl = Mathf.Sin(angle);
        var leftDir = new Vector2(cosl * _agent.Velocity.x - sinl * _agent.Velocity.y, sinl * _agent.Velocity.x + cosl * _agent.Velocity.y);

        Debug.DrawLine(_agent.Position, _agent.Position + leftDir.normalized * _perceptionRadius, Color.yellow);

        hit = Physics2D.Raycast(_agent.Position, leftDir.normalized, _perceptionRadius);
        if (hit.collider != null)
        {
            var targetPos = hit.point + hit.normal * _avoidance;
            return targetPos - _agent.Position;
        }

        var cosr = Mathf.Cos(-angle);
        var sinr = Mathf.Sin(-angle);
        var rightDir = new Vector2(cosr * _agent.Velocity.x - sinr * _agent.Velocity.y, sinr * _agent.Velocity.x + cosr * _agent.Velocity.y);

        Debug.DrawLine(_agent.Position, _agent.Position + rightDir.normalized * _perceptionRadius, Color.yellow);

        hit = Physics2D.Raycast(_agent.Position, rightDir.normalized, _perceptionRadius);
        if (hit.collider != null)
        {
            var targetPos = hit.point + hit.normal * _avoidance;
            return targetPos - _agent.Position;
        }

        return Vector2.zero;
    }

    private Vector2 Flee(Vector2 mousePos)
    {
        var dist = mousePos - _agent.Position;
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

        return averagePoint - _agent.Position;
    }

    public Vector2 CollisionAvoidance(List<Agent2D> agents)
    {
        if (agents.Count == 0)
        {
            return Vector2.zero;
        }

        float maxPredictionDistance = 1;

        float timeToCollision = float.MaxValue;
        Agent2D target = null;

        foreach (var a in agents)
        {
            var relativePos = (_agent.Position - a.Position);
            if (relativePos.sqrMagnitude > maxPredictionDistance * maxPredictionDistance)
            {
                continue;
            }

            var relativeVelocity = (_agent.Velocity - a.Velocity);
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

        if (timeToCollision <= 0 || (_agent.Position - target.Position).sqrMagnitude < _radius * _radius)
        {
            result = _agent.Position - target.Position;
        }
        else
        {
            var pos = _agent.Position - target.Position;
            var vel = _agent.Velocity - target.Velocity;

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
            var interpolation = a.Position - _agent.Position;

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
        Gizmos.DrawWireSphere(_agent.Position, _perceptionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_agent.Position, _separationRadius);
    }
}