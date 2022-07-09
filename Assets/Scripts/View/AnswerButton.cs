using System;
using DG.Tweening;
using Interfaces.Data;
using Interfaces.View;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View
{
    public class AnswerButton : MonoBehaviour, IAnswerButton, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private const float HoverScale = 1.025f;
        
        [SerializeField] private TextMeshProUGUI _answerText;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Color _correctColor;
        [SerializeField] private Color _incorrectColor;
        
        public IAnswer Answer { get; private set; }

        public event Action<IAnswerButton> Clicked;

        public void Initialize(RectTransform parent, IAnswer answer)
        {
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            _buttonImage.color = Color.white;
            _answerText.text = answer.AnswerText;
            Answer = answer;
        }
        
        public void MarkAsCorrectAnswer()
        {
            _buttonImage.DOColor(_correctColor, 0.25f);
        }

        public void MarkAsIncorrectAnswer()
        {
            _buttonImage.DOColor(_incorrectColor, 0.25f);   
        }

        public void OnDespawned()
        {
            Clicked = null;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke(this);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            var hoverScale = Vector3.one * HoverScale;
            transform.DOScale(hoverScale, 0.15f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(Vector3.one, 0.15f);
        }
    }
}