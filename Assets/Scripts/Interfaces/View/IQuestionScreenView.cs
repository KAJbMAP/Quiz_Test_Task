using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Interfaces.View
{
    public interface IQuestionScreenView : IScreenView
    {
        TextMeshProUGUI QuestionText { get; }
        RectTransform AnswersContainer { get; }
        Image QuestionImage { get; }
    }
}