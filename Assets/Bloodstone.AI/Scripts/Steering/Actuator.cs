using UnityEngine;
using UnityEngine.Serialization;

namespace Bloodstone.AI.Steering
{
    public abstract class Actuator : MonoBehaviour
    {
        [SerializeField]
        [FormerlySerializedAs("_agent")]
        protected Agent agent;

        public abstract Vector3 Position { get; set; }
        public abstract Quaternion Rotation { get; set; }

        public abstract Vector3 Velocity { get; set; }
        public abstract Vector3 AngularVelocity { get; set; }

        protected abstract void ActuateMovement();
        protected abstract void ActuateRotation();

        protected virtual void Actuate()
        {
            ActuateMovement();
            ActuateRotation();

            SynchronizeAgent();
        }

        protected void OnEnable()
        {
            SynchronizeAgent();
        }

        protected void SynchronizeAgent()
        {
            agent.Position = Position;
            agent.Rotation = Rotation;
            agent.Velocity = Velocity;
            agent.AngularVelocity = AngularVelocity;
        }
    }
}