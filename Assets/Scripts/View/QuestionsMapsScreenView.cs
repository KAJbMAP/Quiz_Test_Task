using Interfaces.View;
using UnityEngine;

namespace View
{
    public class QuestionsMapsScreenView : BaseScreenView, IQuestionsMapScreenView
    {
        [SerializeField] private RectTransform _tileContainer;

        public RectTransform TileContainer => _tileContainer;
    }
}