using UnityEngine;

namespace Bloodstone.AI
{

    public class Agent2D : MonoBehaviour
    {
        public float MaxSpeed = 0.5f;
        public float MaxAngularVelocity = 1f;

        public bool UseAngularVelocity = true;

        public Vector2 Velocity;
        public float AngularVelocity;

        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }

        public Vector2 Position
        {
            get => transform.position;
            set => transform.position = new Vector3(value.x, value.y, 0);
        }

        private IBehaviour _behaviour;

        private void Awake()
        {
            _behaviour = GetComponent<IBehaviour>();
        }

        private void Update()
        {
            Velocity += _behaviour.GetVelocity();
            Velocity = Vector2.ClampMagnitude(Velocity, MaxSpeed);
            Position += Velocity * Time.deltaTime;

            if (UseAngularVelocity)
            {
                AngularVelocity = _behaviour.GetAngularVelocity();
                AngularVelocity = Mathf.Clamp(AngularVelocity, -MaxAngularVelocity, MaxAngularVelocity);
                Rotation = Quaternion.Euler(0, 0, Rotation.eulerAngles.z + AngularVelocity);
            }
            else
            {
                Rotation = Quaternion.Euler(0, 0, Mathf.Atan2(Velocity.y, Velocity.x) * Mathf.Rad2Deg);
            }

            BoundToScreen();
        }

        private void BoundToScreen()
        {
            const float borderX = 9.5f;
            const float borderY = 5.5f;

            while (Position.x > borderX)
            {
                Position = new Vector2(-borderX, Position.y);
            }
            while (Position.x < -borderX)
            {
                Position = new Vector2(borderX, Position.y);
            }

            while (Position.y > borderY)
            {
                Position = new Vector2(Position.x, -borderY);
            }
            while (Position.y < -borderY)
            {
                Position = new Vector2(Position.x, borderY);
            }
        }
    }
}