using UnityEngine;

namespace Bloodstone.AI.Steering
{
    [ExecuteAlways]
    [RequireComponent(typeof(AISubsystem))]
    [RequireComponent(typeof(Agent))]
    public abstract class SteeringBehaviour : MonoBehaviour, ISteeringBehaviour
    {
        [SerializeField]
        protected bool showGizmos;

        protected AISubsystem subsystem;
        private Agent _agent;
        protected Agent Agent => _agent;

        public abstract Vector3 GetSteering();

        protected virtual void Awake()
        {
            _agent = GetComponent<Agent>();
            subsystem = GetComponent<AISubsystem>();
        }

        protected virtual void DrawGizmos()
        {
        }

        private void OnDrawGizmos()
        {
            if(showGizmos)
            {
                DrawGizmos();
            }
        }
    }

    public abstract class MovementSteering : SteeringBehaviour, IMovementSteering
    {
    }

    public abstract class RotationSteering : SteeringBehaviour, IRotationSteering
    {
    }
}