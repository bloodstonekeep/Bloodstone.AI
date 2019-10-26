using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public interface ISteeringBehaviour
    {
        Vector2 GetVelocity();
    }

    public interface ISteeringPipeline
    {
        Vector2 GetVelocity();
        float GetAngularVelocity();
    }

    public abstract class SteeringBehaviour : ISteeringBehaviour
    {
        public abstract Vector2 GetVelocity();
    }
}