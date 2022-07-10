using System;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Interfaces.Data;
using Interfaces.View;
using Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(RectTransform))]
    public class QuestionMapTile : MonoBehaviour, IQuestionMapTile, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private const float ActiveNormalScale = 1.1f;
        private const float ActiveTileHoverScale = 1.18f;
        private const float InactiveTileHoverScale = 1.05f;

        [Header("Lines")] 
        [SerializeField] private RectTransform _leftLineRect;
        [SerializeField] private RectTransform _upLineRect;
        [SerializeField] private RectTransform _rightLineRect;
        [SerializeField] private RectTransform _downLineRect;

        [Header("Image references")]
        [SerializeField] private Image _tileBackgroundIcon;
        [SerializeField] private Image _categoryIcon;
        [SerializeField] private Image _answeredTileIcon;

        [Header("Color settings")] 
        [SerializeField] private Color _activeCategoryColor;
        [SerializeField] private Color _inactiveCategoryColor;
        [SerializeField] private Color _correctAnsweredTileColor;
        [SerializeField] private Color _incorrectAnsweredTileColor;

        private bool _isActive;
        private Dictionary<TileChilds, RectTransform> _rectTransforms;

        public IQuestionAsset QuestionAsset { get; private set; }
        public Vector2 AnchoredPosition => (transform as RectTransform).anchoredPosition;
        public event Action<IQuestionMapTile> Clicked;

        private void Awake()
        {
            _rectTransforms = new Dictionary<TileChilds, RectTransform>
            {
                {TileChilds.Left, _leftLineRect},
                {TileChilds.Top, _upLineRect},
                {TileChilds.Right, _rightLineRect},
                {TileChilds.Bottom, _downLineRect}
            };
        }

        public void AssignQuestionAsset(IQuestionAsset questionAsset)
        {
            QuestionAsset = questionAsset;
            _tileBackgroundIcon.color = questionAsset.CategoryInfo.CategoryColor;
            _categoryIcon.sprite = questionAsset.CategoryInfo.CategorySprite;
        }
        
        public void MarkAsAnsweredCorrect()
        {
            SetAnsweredCorrectState(true);
            SetInteractableState(false);
        }

        public void MarkAsAnsweredIncorrect()
        {
            SetAnsweredCorrectState(false);
            SetInteractableState(false);
        }

        public void MarkAsActive()
        {
            MarkAsIncomplete();
            SetInteractableState(true);
        }

        public void MarkAsInactive()
        {
            MarkAsIncomplete();
            SetInteractableState(false);
        }

        public void SetNeighboursLines(ITileChildsInfo tileChildsInfo)
        {
            var tileChildsValuesArray = Enum.GetValues(typeof(TileChilds));
            foreach (var tileChild in tileChildsValuesArray)
            {
                if (FlagsHelper.IsSet(tileChildsInfo.TileChilds, (TileChilds) tileChild))
                {
                    var sizeDelta = _rectTransforms[(TileChilds) tileChild].sizeDelta;
                    sizeDelta.x = tileChildsInfo.Distance;
                    _rectTransforms[(TileChilds) tileChild].sizeDelta = sizeDelta;
                }
            }
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isActive) return;
            
            Clicked?.Invoke(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            var hoverScale = _isActive ? Vector3.one * ActiveTileHoverScale : Vector3.one * InactiveTileHoverScale;
            transform.DOScale(hoverScale, 0.15f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            var normalScale = _isActive ? Vector3.one * ActiveNormalScale : Vector3.one;
            transform.DOScale(normalScale, 0.15f);
        }

        private void SetInteractableState(bool isInteractable)
        {
            _isActive = isInteractable;
            transform.localScale = isInteractable ? Vector3.one * ActiveNormalScale : Vector3.one;
            _categoryIcon.color = isInteractable ? _activeCategoryColor : _inactiveCategoryColor;
        }

        private void SetAnsweredCorrectState(bool isAnsweredCorrect)
        {
            _tileBackgroundIcon.enabled = false;
            _categoryIcon.enabled = false;
            _answeredTileIcon.enabled = true;
            _answeredTileIcon.color = isAnsweredCorrect ? _correctAnsweredTileColor : _incorrectAnsweredTileColor;
        }

        private void MarkAsIncomplete()
        {
            _tileBackgroundIcon.enabled = true;
            _categoryIcon.enabled = true;
            _answeredTileIcon.enabled = false;
        }
    }   
}
