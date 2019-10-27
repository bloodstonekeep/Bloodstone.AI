using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public interface ISteeringPipeline
    {
        Vector3 MovementSteering();
        Vector3 RotationSteering();
    }
}