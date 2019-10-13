using UnityEngine;

namespace Bloodstone.AI.Steering
{
    [ExecuteAlways]
    [RequireComponent(typeof(Agent))]
    public abstract class SteeringBehaviour : MonoBehaviour, ISteeringBehaviour
    {
        protected Agent Agent { get; private set; }

        public abstract Vector3 GetSteering();

        protected void Awake()
        {
            Agent = GetComponent<Agent>();
        }
    }

    public abstract class MovementSteering : SteeringBehaviour, IMovementSteering
    {
    }

    public abstract class RotationSteering : SteeringBehaviour, IRotationSteering
    {
    }
}