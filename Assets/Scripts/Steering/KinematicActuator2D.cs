using UnityEngine;

namespace Bloodstone.AI.Steering
{
    public class KinematicActuator2D : MonoBehaviour
    {
        public bool UseAngularVelocity = true;

        [SerializeField]
        private Agent2D _agent;

        private void OnEnable()
        {
            _agent.Position = transform.position;
            _agent.Rotation = transform.rotation;
        }

        private void Update()
        {
            transform.position += new Vector3(_agent.Velocity.x, _agent.Velocity.y, 0) * Time.deltaTime;

            if (UseAngularVelocity)
            {
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + _agent.AngularVelocity);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_agent.Velocity.y, _agent.Velocity.x) * Mathf.Rad2Deg);
            }

            BoundToScreen();

            _agent.Position = transform.position;
            _agent.Rotation = transform.rotation;
        }

        private void BoundToScreen()
        {
            const float borderX = 9.5f;
            const float borderY = 5.5f;

            var currentPosition = transform.position;

            while (currentPosition.x > borderX)
            {
                transform.position = new Vector2(-borderX, currentPosition.y);
            }
            while (currentPosition.x < -borderX)
            {
                transform.position = new Vector2(borderX, currentPosition.y);
            }

            while (currentPosition.y > borderY)
            {
                transform.position = new Vector2(currentPosition.x, -borderY);
            }
            while (currentPosition.y < -borderY)
            {
                transform.position = new Vector2(currentPosition.x, borderY);
            }
        }
    }
}