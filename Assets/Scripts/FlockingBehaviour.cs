using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlockingBehaviour : MonoBehaviour
{
    public bool debug;

    [SerializeField]
    private float _perceptionRadius;
    [SerializeField]
    private float _separationRadius;

    private Agent2D _agent;

    private void Awake()
    {
        _agent = GetComponent<Agent2D>();
    }

    public float GetAngularVelocity()
    {
        if(_agent.Velocity == Vector2.zero)
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

        while(rotationDiff < -180)
        {
            rotationDiff += 360;
        }


        if(Mathf.Abs(rotationDiff) < 5)
        {
            rotationDiff /= 5;
        }

        if(debug)
        {
            Debug.Log($"TargetRotation: {targetRotation} | Current: {currentRotation} | diff: {rotationDiff}");
        }

        return rotationDiff;
    }

    public Vector2 GetVelocity()
    {
        var nearbyAgents = BoidsSpawner.Agents.Where(a => a.gameObject != gameObject)
                             .Where(a => (a.Position - _agent.Position).sqrMagnitude < _perceptionRadius * _perceptionRadius)
                             .ToList();

        var v1 = Cohesion(nearbyAgents) * BoidsSpawner.CohesionWeight;
        var v2 = Separation(nearbyAgents) * BoidsSpawner.SeparationWeight;
        var v3 = VelocityMatch(nearbyAgents) * BoidsSpawner.VelocityMatchWeight;

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var v4 = Flee(mousePos);

        return (v1 + v2 * 2f + v3 + v4 * 6f) / 4;
    }

    private Vector2 Flee(Vector2 mousePos)
    {
        var dist = mousePos - _agent.Position;
        if(dist.sqrMagnitude < _separationRadius * _separationRadius + 1)
        {
            return -dist;
        }

        return Vector2.zero;
    }

    public Vector2 Cohesion(List<Agent2D> agents)
    {
        Vector2 averagePoint = Vector2.zero;
        if(agents.Count == 0)
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

    public Vector2 Separation(List<Agent2D> agents)
    {
        Vector2 separationVector = Vector2.zero;
        int separatedCount = 0;
        foreach (var a in agents)
        {
            var interpolation = a.Position - _agent.Position;

            if(interpolation.sqrMagnitude < _separationRadius * _separationRadius)
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

        if(nearbyBoids.Count == 0)
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