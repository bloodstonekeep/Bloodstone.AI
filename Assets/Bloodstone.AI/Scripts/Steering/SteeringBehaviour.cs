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

        protected Agent Agent { get; private set; }

        public abstract Vector3 GetSteering();

        protected virtual void Awake()
        {
            Agent = GetComponent<Agent>();
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
}