using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public sealed class KinematicActuator : Actuator
    {
        public override Vector3 Velocity { get; set; }
        public override Vector3 AngularVelocity { get; set; }

        public override Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }

        public override Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        protected override void ActuateMovement()
        {
            Velocity += agent.Prediction.velocity;
            Velocity = Vector3.ClampMagnitude(Velocity, agent.Statistics.speed);
            Position += Velocity * Time.deltaTime;
        }

        protected override void ActuateRotation()
        {
            AngularVelocity = agent.Prediction.angularVelocity;
            AngularVelocity = Vector3.ClampMagnitude(AngularVelocity, agent.Statistics.angularSpeed);
            Rotation *= Quaternion.Euler(AngularVelocity * Time.deltaTime);
        }

        private void Update()
        {
            Actuate();
        }
    }
}