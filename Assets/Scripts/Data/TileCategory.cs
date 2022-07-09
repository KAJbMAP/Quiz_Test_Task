using Enums;
using Interfaces.Data;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class QuestionCategoryInfo : IQuestionCategoryInfo
    {
        [SerializeField] private QuestionCategory _questionCategory;
        [SerializeField] private Sprite _categorySprite;
        [SerializeField] private Color _categoryColor;

        public QuestionCategory QuestionCategory => _questionCategory;
        public Sprite CategorySprite => _categorySprite;
        public Color CategoryColor => _categoryColor;

        public QuestionCategoryInfo(QuestionCategory questionCategory, Sprite categorySprite, Color categoryColor)
        {
            _questionCategory = questionCategory;
            _categorySprite = categorySprite;
            _categoryColor = categoryColor;
        }
    }
}