using System;
using System.Collections;
using UnityEngine;

namespace Bloodstone.AI.Examples.Boids.UI
{
    public class CanvasFader : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvas;

        [SerializeField]
        [Tooltip("x = alpha when in Hidden state, y = alpha when in Visible state")]
        private Vector2 _fadeAlphaRange = new Vector2(0, 1);

        [SerializeField]
        private float _fadeDuration = 0.2f;

        private Coroutine _currentCoroutine;

        public Status CurrentStatus { get; private set; }

        private float AlphaWhenVisible => _fadeAlphaRange.y;
        private float AlphaWhenHidden => _fadeAlphaRange.x;

        public void Show()
        {
            StartFading(Fade.In, Status.Visible);
        }

        public void Hide()
        {
            StartFading(Fade.Out, Status.Hidden);
        }

        private void StartFading(Fade mode, Status newStatus)
        {
            if (CurrentStatus == newStatus)
            {
                return;
            }

            RestartFadeCoroutine(mode);
            CurrentStatus = newStatus;
        }

        private void RestartFadeCoroutine(Fade fade)
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            _currentCoroutine = StartCoroutine(FadeCanvasGroup(fade));
        }

        private IEnumerator FadeCanvasGroup(Fade fade)
        {
            OnBeforeFade();

            var (startAlpha, endAlpha)= GetFadeRangeAlpha(fade);

            float currentStep = CalculateCurrentStep(endAlpha);
            for (; currentStep < 1f; currentStep += Time.deltaTime / _fadeDuration)
            {
                _canvas.alpha = Mathf.Lerp(startAlpha, endAlpha, currentStep);
                yield return null;
            }
            _canvas.alpha = endAlpha;

            OnAfterFade(fade);

        }

        private float CalculateCurrentStep(float endValue)
        {
            var sign = Mathf.Sign(endValue - _canvas.alpha);

            var currentProgress = _canvas.alpha / AlphaWhenVisible;
            return sign >= 0 ? currentProgress : 1 - currentProgress;
        }

        private (float start, float end) GetFadeRangeAlpha(Fade fade)
        {
            switch (fade)
            {
                case Fade.Out:
                    return (AlphaWhenVisible, AlphaWhenHidden);
                case Fade.In:
                    return (AlphaWhenHidden, AlphaWhenVisible);
                default:
                    throw new NotSupportedException();
            }
        }

        private void OnBeforeFade()
        {
            _canvas.interactable = false;
        }

        private void OnAfterFade(Fade fade)
        {
            _currentCoroutine = null;
            bool isVisible = fade == Fade.In;

            SetVisibility(isVisible);
        }

        private void SetVisibility(bool isVisible)
        {
            _canvas.blocksRaycasts = isVisible;
            _canvas.interactable = isVisible;
            _canvas.alpha = isVisible ? AlphaWhenVisible : AlphaWhenHidden;
        }

        public enum Status { Visible, Hidden }

        private enum Fade { In, Out }
    }
}