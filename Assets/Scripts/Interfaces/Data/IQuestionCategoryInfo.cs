using Enums;
using UnityEngine;

namespace Interfaces.Data
{
    public interface IQuestionCategoryInfo
    {
        QuestionCategory QuestionCategory { get; }
        Sprite CategorySprite { get; }
        Color CategoryColor { get; }
    }
}