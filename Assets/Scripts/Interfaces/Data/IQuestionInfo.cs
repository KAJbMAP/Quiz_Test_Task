using UnityEngine;

namespace Interfaces.Data
{
    
    public interface IQuestionInfo
    {
        Sprite QuestionSprite { get; }
        string Question { get; }
        IAnswer[] Answers { get; }
    }
}