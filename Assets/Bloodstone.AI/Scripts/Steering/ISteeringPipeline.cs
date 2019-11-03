using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public interface ISteeringPipeline
    {
        Vector3 GetMovementSteering();
        Vector3 GetRotationSteering();
    }
}