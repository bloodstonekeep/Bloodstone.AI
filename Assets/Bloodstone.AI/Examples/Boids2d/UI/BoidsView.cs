using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bloodstone.AI.Examples.Boids
{
    public class BoidsView : MonoBehaviour
    {
        [SerializeField]
        private BoidSteeringWeights _weight;

        [SerializeField]
        private GameObject _togglablePanel;
        [SerializeField]
        private GameObject _showButton;

        [Space(5)]

        [SerializeField]
        private Slider _cohesionSlider;

        [SerializeField]
        private Slider _separationSlider;

        [SerializeField]
        private Slider _velocityMatchSlider;

        [SerializeField]
        private Slider _collisionAvoidanceSlider;

        private void Awake()
        {
            _cohesionSlider.value = _weight.Cohesion;
            _separationSlider.value = _weight.Separation;
            _velocityMatchSlider.value = _weight.VelocityMatch;
            _collisionAvoidanceSlider.value = _weight.CollisionAvoidance;
        }

        internal void Hide()
        {
            _togglablePanel.SetActive(false);
            _showButton.SetActive(true);
        }

        internal void Show()
        {
            _togglablePanel.SetActive(true);
            _showButton.SetActive(false);
        }
    }
}