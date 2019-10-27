using UnityEngine;

namespace Bloodstone.AI
{
    public class KinematicActuator : MonoBehaviour
    {
        [SerializeField]
        private Agent _agent;

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

        private Vector3 _velocity;

        private Vector3 _angularVelocity;

        private void OnEnable()
        {
            UpdateAgent();
        }

        private void UpdateAgent()
        {
            _agent.Position = Position;
            _agent.Rotation = Rotation;
            _agent.Velocity = _velocity;
            _agent.AngularVelocity = _angularVelocity;
        }

        private void Update()
        {
            _velocity += _agent.Prediction.Velocity;
            _velocity = Vector3.ClampMagnitude(_velocity, _agent.Statistics.MaximumSpeed);
            Position += _velocity * Time.deltaTime;

            _angularVelocity = _agent.Prediction.AngularVelocity;
            _angularVelocity = Vector3.ClampMagnitude(_angularVelocity, _agent.Statistics.MaximumAngularSpeed);
            Rotation *= Quaternion.Euler(_angularVelocity * Time.deltaTime);

            UpdateAgent();
        }
    }
}