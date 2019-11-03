using UnityEngine;
using UnityEngine.UI;

namespace Bloodstone.AI.Examples.Boids.UI
{
    public class BoidsView : MonoBehaviour
    {
        [Header("Boids steering weights model")]
        [SerializeField]
        private BoidSteeringWeights _steeringWeights;

        [Header("UI components")]

        [SerializeField]
        private CanvasFader _settingsFader;

        [SerializeField]
        private CanvasFader _inGameFader;

        [SerializeField]
        private Slider _cohesionSlider;

        [SerializeField]
        private Slider _separationSlider;

        [SerializeField]
        private Slider _velocityMatchSlider;

        [SerializeField]
        private Slider _collisionAvoidanceSlider;

        internal void Hide()
        {
            _settingsFader.Hide();
            _inGameFader.Show();
        }

        internal void Show()
        {
            _settingsFader.Show();
            _inGameFader.Hide();
        }

        private void Awake()
        {
            _cohesionSlider.value = _steeringWeights.cohesion;
            _separationSlider.value = _steeringWeights.separation;
            _velocityMatchSlider.value = _steeringWeights.velocityMatch;
            _collisionAvoidanceSlider.value = _steeringWeights.collisionAvoidance;
        }
    }
}