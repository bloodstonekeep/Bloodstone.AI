using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public interface ISteeringBehaviour
    {
        Vector3 GetSteering();
    }

    public interface IRotationSteering : ISteeringBehaviour
    {
    }

    public interface IMovementSteering : ISteeringBehaviour
    {
    }
}