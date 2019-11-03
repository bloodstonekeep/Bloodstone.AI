using UnityEngine;

namespace Bloodstone.AI.Examples.Boids.UI
{
    [RequireComponent(typeof(BoidsView))]
    public class BoidsController : MonoBehaviour
    {
        [SerializeField]
        private bool _startActive;

        [SerializeField]
        private BoidsView _view;

        [Space(5)]
        [SerializeField]
        private BoidsSymulation _boidsSymulation;

        [SerializeField]
        private BoidSteeringWeights _weight;

        public void Show()
        {
            _view.Show();
        }

        public void Hide()
        {
            _view.Hide();
        }

        public void ApplicationQuit()
        {
            Application.Quit();
        }

        public void SetBoidsCohesion(float value)
        {
            _weight.cohesion = value;
        }

        public void SetBoidsSeparation(float value)
        {
            _weight.separation = value;
        }

        public void SetBoidsVelocityMatch(float value)
        {
            _weight.velocityMatch = value;
        }

        public void SetBoidsCollisionAvoidance(float value)
        {
            _weight.collisionAvoidance = value;
        }

        public void AddBoids(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                _boidsSymulation.AddBoid();
            }
        }

        public void RemoveBoids(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                _boidsSymulation.RemoveBoid();
            }
        }

        public void SwitchBorderMode(bool value)
        {
            _boidsSymulation.SwitchBorderMode(value);
        }

        private void Start()
        {
            if (_startActive)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void OnValidate()
        {
            if(_view == null)
            {
                _view = GetComponent<BoidsView>();
            }
        }
    }
}