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

        private void Update()
        {
            var newVelocity = _agent.Prediction.Velocity;
            newVelocity = Vector3.ClampMagnitude(newVelocity, _agent.Statistics.MaximumSpeed);
            Position += newVelocity * Time.deltaTime;

            var newAngularVelocity = _agent.Prediction.AngularVelocity;
            newAngularVelocity = Vector3.ClampMagnitude(newAngularVelocity, _agent.Statistics.MaximumAngularSpeed);
            Rotation *= Quaternion.Euler(newAngularVelocity * Time.deltaTime);

            _agent.Position = Position;
            _agent.Rotation = Rotation;
            _agent.Velocity = newVelocity;
            _agent.AngularVelocity = newAngularVelocity;
        }
    }
}