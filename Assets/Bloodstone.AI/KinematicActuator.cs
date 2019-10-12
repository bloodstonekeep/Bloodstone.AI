using UnityEngine;

namespace Bloodstone.AI
{
    [RequireComponent(typeof(Agent))]
    public class KinematicActuator : MonoBehaviour
    {
        private Agent _agent;

        [SerializeField]
        private Vector3 _velocity;

        [SerializeField]
        private Vector3 _angularVelocity;

        private void Awake()
        {
            _agent = GetComponent<Agent>();
        }

        public Vector3 Velocity
        {
            get => _velocity;
            set => _velocity = value;
        }

        public Vector3 AngularVelocity
        {
            get => _angularVelocity;
            set => _angularVelocity = value;
        }

        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = new Vector3(value.x, value.y, 0);
        }

        private void Update()
        {
            Velocity += _agent.Prediction.Velocity;
            Velocity = Vector3.ClampMagnitude(Velocity, _agent.Statistics.MaximumSpeed);
            Position += Velocity * Time.deltaTime;

            AngularVelocity = _agent.Prediction.AngularVelocity;
            AngularVelocity = Vector3.ClampMagnitude(AngularVelocity, _agent.Statistics.MaximumAngularSpeed);
            Rotation = Quaternion.Euler(Rotation.eulerAngles + AngularVelocity);
        }
    }
}