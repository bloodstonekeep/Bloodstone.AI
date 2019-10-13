using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public abstract class SteeringBehaviour : MonoBehaviour, ISteeringBehaviour
    {
        public abstract Vector3 GetSteering();
    }

    public abstract class MovementSteering : SteeringBehaviour, IMovementSteering
    {
    }

    public abstract class RotationSteering : SteeringBehaviour, IRotationSteering
    {
    }
}