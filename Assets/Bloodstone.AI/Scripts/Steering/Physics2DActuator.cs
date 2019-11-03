using UnityEngine;

namespace Bloodstone.AI.Steering
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Physics2DActuator : Actuator
    {
        private Rigidbody2D _rigidbody;

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

        public override Vector3 Velocity
        {
            get => _rigidbody.velocity;
            set => _rigidbody.velocity = value;
        }

        public override Vector3 AngularVelocity
        {
            get => new Vector3(0, 0, _rigidbody.angularVelocity);
            set => _rigidbody.angularVelocity = value.z;
        }

        protected override void ActuateMovement()
        {
            var newVelocity = Vector3.ClampMagnitude(agent.Prediction.velocity, agent.Statistics.speed);
            Velocity += new Vector3(newVelocity.x, newVelocity.y) * Time.fixedDeltaTime;
        }

        protected override void ActuateRotation()
        {
            AngularVelocity = Vector3.ClampMagnitude(agent.Prediction.angularVelocity, agent.Statistics.angularSpeed);
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Actuate();
        }
    }
}