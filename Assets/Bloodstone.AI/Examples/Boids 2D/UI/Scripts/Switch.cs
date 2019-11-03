using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Events;
using System;
using TMPro;

namespace Bloodstone.AI.Examples.Boids.UI
{
    [RequireComponent(typeof(Switch))]
    public class Switch : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private TMP_Text _optionLabel;

        [SerializeField]
        private string _onLabel;

        [SerializeField]
        private string _offLabel;

        [Header("Color settings")]
        [SerializeField]
        private Color _handleColorOn = Color.white;

        [SerializeField]
        private Color _handleColorOff = Color.gray;

        [Space(10)]
        [Header("Feature settings")]

        [Range(0f, 3f)]
        [SerializeField]
        private float _animationTime;

        [SerializeField]
        private bool _isOn = true;

        [Tooltip("Check if user can turn ON this switch")]
        public bool canTurnOn = true;

        [Tooltip("Check if user can turn OFF this switch")]
        public bool canTurnOff = true;

        [SerializeField]
        private SwitchEvent _onSwitchEvent;

        private Slider _slider;
        private Image _handle;

        public bool IsOn => _isOn;

        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
            _slider.interactable = false;
            _handle = _slider.handleRect.GetComponent<Image>();

            if (_isOn)
            {
                TurnOn();
            }
            else
            {
                TurnOff();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SwitchMode();
        }

        private void TurnOn()
        {
            StopAllCoroutines();
            StartCoroutine(SwitchMode(_handleColorOff, _handleColorOn, _slider.minValue, _slider.maxValue));


            _isOn = true;
            _optionLabel.text = _onLabel;
            _onSwitchEvent.Invoke(_isOn);
        }

        private void TurnOff()
        {
            StopAllCoroutines();
            StartCoroutine(SwitchMode(_handleColorOn, _handleColorOff, _slider.maxValue, _slider.minValue));

            _isOn = false;
            _optionLabel.text = _offLabel;
            _onSwitchEvent.Invoke(_isOn);
        }

        public void SwitchMode()
        {
            if (_isOn && canTurnOff)
            {
                TurnOff();
            }
            else if (canTurnOn)
            {
                TurnOn();
            }
        }

        IEnumerator SwitchMode(Color startColor, Color endColor, float startValue, float endValue)
        {
            if (_animationTime != 0)
            {
                for (float t = 0; t < 1f; t += Time.deltaTime / _animationTime)
                {
                    _slider.value = Mathf.Lerp(startValue, endValue, t);
                    _handle.color = Color.Lerp(startColor, endColor, t);

                    yield return null;
                }
            }

            _slider.value = endValue;
            _handle.color = endColor;
        }

        [Serializable]
        public class SwitchEvent : UnityEvent<bool>
        {
        }
    }
}