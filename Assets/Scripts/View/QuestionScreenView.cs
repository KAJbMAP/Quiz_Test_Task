using Interfaces.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class QuestionScreenView : BaseScreenView, IQuestionScreenView
    {
        [SerializeField] private TextMeshProUGUI _questionText;
        [SerializeField] private RectTransform _answersContainer;
        [SerializeField] private Image _questionImage;
        
        public TextMeshProUGUI QuestionText => _questionText;
        public RectTransform AnswersContainer => _answersContainer;
        public Image QuestionImage => _questionImage;
    }
}