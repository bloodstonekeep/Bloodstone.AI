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

        public void ApplicationQuit()
        {
            Application.Quit();
        }

        public void SetBoidsCohesion(float value)
        {
            _weight.Cohesion = value;
        }

        public void SetBoidsSeparation(float value)
        {
            _weight.Separation = value;
        }

        public void SetBoidsVelocityMatch(float value)
        {
            _weight.VelocityMatch = value;
        }

        public void SetBoidsCollisionAvoidance(float value)
        {
            _weight.CollisionAvoidance = value;
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

        public void Show()
        {
            _view.Show();
        }

        public void Hide()
        {
            _view.Hide();
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