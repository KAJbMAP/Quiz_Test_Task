using System;
using Interfaces.Data;

namespace Interfaces.View
{
    public interface IAnswerButton
    {
        IAnswer Answer { get; }
        void MarkAsCorrectAnswer();
        void MarkAsIncorrectAnswer();
        event Action<IAnswerButton> Clicked;
    }
}