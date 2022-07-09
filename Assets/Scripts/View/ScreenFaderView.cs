using System;
using DG.Tweening;
using Interfaces.View;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ScreenFaderView : MonoBehaviour, IScreenFaderView
    {
        [SerializeField] private Image _blockImage;
        [SerializeField] private RectTransform _fadedRect;
        [SerializeField] private float _fadeTime;

        private bool _tweenWithHeight;
        private Sequence _sequence;

        public void Update()      
        {
            if (Input.GetKeyDown(KeyCode.F))
                FadeInOut();
        }


        public void FadeInOut(Action fullFadedCallback = null)
        {
            if (_sequence != null && _sequence.IsActive())
                _sequence?.Kill();

            if (_tweenWithHeight) TweenWithHeight(fullFadedCallback);
            else TweenWithWidth(fullFadedCallback);

            _tweenWithHeight = !_tweenWithHeight;
        }


        private void TweenWithWidth(Action fullFadedCallback = null)
        {
            _fadedRect.localScale = new Vector3(0f, 1f, 1f);
            _blockImage.raycastTarget = true;

            _sequence = DOTween.Sequence();
            _sequence.Append(_fadedRect.DOScaleX(1f, _fadeTime / 2f));
            _sequence.AppendCallback(() => fullFadedCallback?.Invoke());
            _sequence.AppendInterval(_fadeTime / 5f);
            _sequence.Append(_fadedRect.DOScaleY(0f, _fadeTime / 2f));
            _sequence.OnComplete(() => _blockImage.raycastTarget = false);
            _sequence.SetEase(Ease.InOutQuad);
        }
        
        private void TweenWithHeight(Action fullFadedCallback = null)
        {
            _fadedRect.localScale = new Vector3(1f, 0f, 1f);
            _blockImage.raycastTarget = true;

            _sequence = DOTween.Sequence();
            _sequence.Append(_fadedRect.DOScaleY(1f, _fadeTime / 2f));
            _sequence.AppendCallback(() => fullFadedCallback?.Invoke());
            _sequence.AppendInterval(_fadeTime / 5f);
            _sequence.Append(_fadedRect.DOScaleX(0f, _fadeTime / 2f));
            _sequence.OnComplete(() => _blockImage.raycastTarget = false);
            _sequence.SetEase(Ease.InOutQuad);
        }
    }
}