using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Events;
using System;

namespace Bloodstone.UI
{
    public class Switch : MonoBehaviour, IPointerDownHandler
    {
        public Text TextUnder;

        [Header("Color settings")]
        public Color fillColor = Color.white;

        public Color handleColorOn = Color.white;
        public Color handleColorOff = Color.gray;

        [Space(10)]
        [Header("Feature settings")]

        [Range(0f, 3f)]
        [SerializeField]
        private float _animationTime;

        [Tooltip("Check this if switch have to be ON from start")]
        public bool IsOn = true;

        [Tooltip("Check if user can turn ON this switch")]
        public bool CanClickOn = true;

        [Tooltip("Check if user can turn OFF this switch")]
        public bool CanClickOff = true;

        [SerializeField]
        private SwitchEvent _onSwitchEvent;

        private Slider _slider;
        private Image _handle;

        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
            _handle = _slider.handleRect.GetComponent<Image>();

            if (IsOn)
            {
                TurnOn();
            }
            else
            {
                TurnOff();
            }
        }

        /// <summary>
        /// Handles pointer down on element
        /// </summary>
        /// <param name="eventData">Pointer's event data</param>
        public void OnPointerDown(PointerEventData eventData)
        {
            SwitchMode();
        }

        private void TurnOn()
        {
            StopAllCoroutines();
            StartCoroutine(SwitchMode(handleColorOff, handleColorOn, _slider.minValue, _slider.maxValue));


            IsOn = true;
        }

        private void TurnOff()
        {
            StopAllCoroutines();
            StartCoroutine(SwitchMode(handleColorOn, handleColorOff, _slider.maxValue, _slider.minValue));

            IsOn = false;
        }

        public void SwitchMode()
        {
            if (IsOn)
            {
                if (CanClickOff)
                {
                    TurnOff();
                }
            }
            else
            {
                if (CanClickOn)
                {
                    TurnOn();
                }
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