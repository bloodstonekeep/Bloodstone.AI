using UnityEngine;

namespace Bloodstone.AI
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Physics2DActuator : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private Agent _agent;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        private void FixedUpdate()
        {
            var newVelocity = _agent.Prediction.Velocity;
            newVelocity = Vector3.ClampMagnitude(newVelocity, _agent.Statistics.MaximumSpeed);
            _rigidbody.velocity += new Vector2(newVelocity.x, newVelocity.y) * Time.fixedDeltaTime;

            var newAngularVelocity = _agent.Prediction.AngularVelocity;
            newAngularVelocity = Vector3.ClampMagnitude(newAngularVelocity, _agent.Statistics.MaximumAngularSpeed);
            _rigidbody.angularVelocity += newAngularVelocity.z * Time.fixedDeltaTime;

            _agent.Position = Position;
            _agent.Rotation = Rotation;
            _agent.Velocity = newVelocity;
            _agent.AngularVelocity = newAngularVelocity;
        }
    }
}