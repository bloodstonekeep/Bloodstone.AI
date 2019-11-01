using Bloodstone.AI.Examples.Boids.UI;
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
        private CanvasFader _settingsFader;
        [SerializeField]
        private CanvasFader _showButtonFader;

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
            _settingsFader.Hide();
            _showButtonFader.Show();
        }

        internal void Show()
        {
            _settingsFader.Show();
            _showButtonFader.Hide();
        }
    }
}